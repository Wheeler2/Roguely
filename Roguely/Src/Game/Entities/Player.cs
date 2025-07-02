using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Roguely.Core;
using Roguely.Core.Components;
using Roguely.Core.Entities;
using Roguely.Core.Input;

namespace Roguely.Entities;

public class Player : Entity
{
    public Player()
    {
        Texture2D texture = GameManager.Instance.Content.Load<Texture2D>("Sprites/Barbarian");
        Sprite sprite = new Sprite(texture, Color.White, new Vector2(texture.Width / 2, texture.Height / 2));

        AddComponent(new Transform());
        AddComponent(new SpriteRenderer(sprite));
    }

    protected override void Update()
    {
        Transform transform = GetComponent<Transform>();
        if (transform == null)
            return;

        // Update the player's position based on input
        Vector2 moveDirection = InputManager.MoveDirection;
        transform.Position += moveDirection * 100 * Time.DeltaTime;

        if (Camera.Main.TryGetComponent(out Transform cameraTransform))
        {
            // Center the camera on the player
            cameraTransform.Position = GetComponent<Transform>().Position;
        }
    }
}
