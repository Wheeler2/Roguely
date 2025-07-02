using Microsoft.Xna.Framework;

namespace Roguely.Core.Components;

public class SquareCollider : Collider
{
    public Rectangle Bounds { get; private set; }

    public SquareCollider(Vector2 size)
    {
        Bounds = new Rectangle(0, 0, (int)size.X, (int)size.Y);
    }
}