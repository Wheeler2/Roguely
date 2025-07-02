using System;
using System.Collections.Generic;

namespace Roguely.Core.Entities;

public static class EntityManager
{
    private static readonly Dictionary<Guid, Entity> _entityLookup = new();
    private static readonly Queue<Entity> _entitiesToAdd = new();
    private static readonly Queue<Entity> _entitiesToRemove = new();

    public static IEnumerable<Entity> GetEntities() => _entityLookup.Values;

    public static Entity GetEntityById(Guid id)
    {
        if (_entityLookup.TryGetValue(id, out var entity))
            return entity;

        throw new KeyNotFoundException($"Entity with ID {id} not found.");
    }

    public static void RegisterEntity(Entity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _entitiesToAdd.Enqueue(entity);
    }

    public static void RemoveEntity(Entity entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));

        _entitiesToRemove.Enqueue(entity);
    }

    public static void UpdateEntities()
    {
        // Process pending removals first
        while (_entitiesToRemove.Count > 0)
        {
            var entity = _entitiesToRemove.Dequeue();
            if (_entityLookup[entity.Id] == entity)
            {
                _entityLookup.Remove(entity.Id);
            }
        }

        // Process pending additions
        while (_entitiesToAdd.Count > 0)
        {
            var entity = _entitiesToAdd.Dequeue();
            if (!_entityLookup.ContainsKey(entity.Id))
            {
                _entityLookup[entity.Id] = entity;
            }
        }

        // Update all entities
        foreach (var entity in _entityLookup.Values)
        {
            if (entity.Enabled)
                entity.BaseUpdate();
        }
    }
}