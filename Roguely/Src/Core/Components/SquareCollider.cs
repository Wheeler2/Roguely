using Microsoft.Xna.Framework;

namespace Roguely;

public class SquareCollider : Collider
{
    public BoundingBox BoundingBox { get; private set; }

    public SquareCollider(Vector2 size)
    {
        BoundingBox = new BoundingBox(
            new Vector3(-size.X / 2, -size.Y / 2, 0),
            new Vector3(size.X / 2, size.Y / 2, 0)
        );
    }
}