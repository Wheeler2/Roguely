namespace Roguely.Core;

public class Component : IComponent
{
    public virtual void Destroy() { }

    public virtual void Init() { }

    public virtual void Update() { }
}