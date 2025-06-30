using System;
using System.Collections.Generic;

namespace Roguely.Core.Entities;

public abstract class Entity
{
    public readonly Guid Id;
    public Entity()
    {
        Id = Guid.NewGuid();
        _components = new HashSet<IComponent>();
    }

    protected HashSet<IComponent> _components = new();

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
        if (_components.Contains(component))
            throw new InvalidOperationException("Component already added to this entity.");
        _components.Add(component);
        component.Init();
        return this;
    }

    public Entity RemoveComponent(IComponent component)
    {
        if (component == null)
            throw new ArgumentNullException(nameof(component));
        if (!_components.Contains(component))
            throw new InvalidOperationException("Component not found in this entity.");
        _components.Remove(component);
        component.Destroy();
        return this;
    }

    public bool HasComponent<T>() where T : IComponent
    {
        foreach (var component in _components)
        {
            if (component is T)

                return true;
        }
        return false;
    }

    public T GetComponent<T>() where T : IComponent
    {
        foreach (var component in _components)
        {
            if (component is T typedComponent)
                return typedComponent;
        }
        throw new InvalidOperationException($"Component of type {typeof(T).Name} not found in this entity.");
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
    /// <param name="deltaTime">The time elapsed since the last update.</param>
    public void BaseUpdate(float deltaTime)
    {
        foreach (var component in _components)
        {
            component.Update(deltaTime);
        }

        Update(deltaTime);
    }

    protected virtual void OnEnabled() { }
    protected virtual void OnDisabled() { }
    protected virtual void Update(float deltaTime) { }
}