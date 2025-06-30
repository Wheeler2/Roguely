using Microsoft.Xna.Framework;
using Roguely.Core;

namespace Roguely;

public class Transform : IComponent
{
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public Vector2 Scale { get; set; }

    public void Destroy() { }
    public void Init() { }
    public void Update(float deltaTime) { }
}