using System;
using System.Collections.Generic;
using Roguely.Core.Components;
using Roguely.Core.Entities;

namespace Roguely.Core;

public class Entity : IDisposable
{
    public readonly Guid Id;
    public Entity()
    {
        Id = Guid.NewGuid();
        _components = new Dictionary<Type, IComponent>();

        EntityManager.RegisterEntity(this);
    }

    ~Entity()
    {
        Dispose();
    }

    protected Dictionary<Type, IComponent> _components = new();

    /// <summary>
    /// Indicates whether the entity is enabled. If false, the entity will not be updated or
    /// processed by the game loop.
    /// </summary>
    /// <remarks>
    /// This property should only be set via the SetEnabled method. It is public to allow
    /// external systems to check if the entity is enabled, but it should not be modified directly.
    /// </remarks>
    public bool Enabled { get; private set; } = true; // Should only ever be set via the SetEnabled method.

    public Entity AddComponent(IComponent component)
    {
        if (component == null)
            throw new ArgumentNullException(nameof(component));

        if (_components.ContainsKey(component.GetType()))
            throw new InvalidOperationException("Component already added to this entity.");

        // Automatically set parent entity if the component supports it
        if (component is SpriteRenderer spriteRenderer)
            spriteRenderer.SetParentEntity(this);

        _components.Add(component.GetType(), component);
        component.Init();

        return this;
    }

    public Entity RemoveComponent(IComponent component)
    {
        if (component == null)
            throw new ArgumentNullException(nameof(component));

        if (!_components.ContainsKey(component.GetType()))
            throw new InvalidOperationException("Component not found in this entity.");

        _components.Remove(component.GetType());
        component.Destroy();

        return this;
    }

    public bool HasComponent<T>() where T : IComponent
    {
        return _components.TryGetValue(typeof(T), out var component) && component != null;
    }

    public T GetComponent<T>() where T : IComponent
    {
        if (_components.TryGetValue(typeof(T), out var foundComponent) && foundComponent != null)
        {
            return (T)foundComponent;
        }
        throw new InvalidOperationException($"Component of type {typeof(T).Name} not found in this entity.");
    }

    public bool TryGetComponent<T>(out T component) where T : IComponent
    {
        if (_components.TryGetValue(typeof(T), out var foundComponent) && foundComponent != null)
        {
            component = (T)foundComponent;
            return true;
        }
        component = default;
        return false;
    }

    public void SetEnabled(bool enabled)
    {
        Enabled = enabled;
        if (enabled)
        {
            OnEnabled();
        }
        else if (!enabled)
        {
            OnDisabled();
        }
    }

    /// <summary>
    /// Called by the game loop to update the entity and its components.
    /// This method should be called by the game manager or main loop.
    /// It will call the Update method of each component and then the entity's own Update method.
    /// </summary>
    public void BaseUpdate()
    {
        foreach (var component in _components.Values)
        {
            component.Update();
        }

        Update();
    }

    protected virtual void OnEnabled() { }
    protected virtual void OnDisabled() { }
    protected virtual void Update() { }

    public void Dispose()
    {
        // Clean up components
        foreach (var component in _components.Values)
        {
            component.Destroy();
        }
        _components.Clear();

        // Unregister from the entity manager
        EntityManager.UnregisterEntity(this);

        GC.SuppressFinalize(this);
    }
}