using Microsoft.Xna.Framework;

namespace Roguely.Core.Components;

public class Transform : IComponent
{
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;

    public void Destroy() { }
    public void Init() { }
    public void Update() { }
}