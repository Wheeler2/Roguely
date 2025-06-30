using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Roguely.Rendering;
using Roguely.Core.Entities;

namespace Roguely;

public class Player : Entity
{
    public Player()
    {
        Texture2D spr = GameManager.Instance.Content.Load<Texture2D>("Sprites/Barbarian");
        Transform tr = new Transform();
        AddComponent(tr)
        .AddComponent(new SpriteRenderer(spr, tr));
    }

    protected override void Update(float deltaTime)
    {
        KeyboardState state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.W))
        {
            Transform transform = GetComponent<Transform>();
            transform.Position += new Vector2(0, -100) * deltaTime;
        }
        if (state.IsKeyDown(Keys.S))
        {
            Transform transform = GetComponent<Transform>();
            transform.Position += new Vector2(0, 100) * deltaTime;
        }
        if (state.IsKeyDown(Keys.A))
        {
            Transform transform = GetComponent<Transform>();
            transform.Position += new Vector2(-100, 0) * deltaTime;
        }
        if (state.IsKeyDown(Keys.D))
        {
            Transform transform = GetComponent<Transform>();
            transform.Position += new Vector2(100, 0) * deltaTime;
        }
    }
}
