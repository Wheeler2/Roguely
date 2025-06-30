using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Roguely.Core;

namespace Roguely.Rendering;

public class SpriteRenderer : IComponent
{
    public SpriteRenderer(Texture2D texture, Vector2 position)
    {
        Texture = texture;
        _position = position;
    }

    public SpriteRenderer(Texture2D texture, Transform parentTransform)
    {
        Texture = texture;
        _parentTransform = parentTransform;
        _position = parentTransform.Position;
    }

    public Texture2D Texture { get; set; }
    public Color Color { get; set; } = Color.White;

    private Transform _parentTransform;
    private Vector2 _position;

    public void Update(float deltaTime)
    {
        if (_parentTransform != null)
        {
            _position = _parentTransform.Position;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Texture == null)
            return;

        spriteBatch.Draw(Texture, _position, Color);

        if (_parentTransform != null)
        {
            spriteBatch.Draw(Texture, _position, null, Color, _parentTransform.Rotation, Vector2.Zero, _parentTransform.Scale, SpriteEffects.None, 0f);
        }
    }

    public void Init()
    {
        if (_parentTransform != null)
        {
            _position = _parentTransform.Position;
        }

        GameManager.Instance.RegisterRenderer(this);
    }

    public void Destroy()
    {
        GameManager.Instance.UnregisterRenderer(this);
        Texture = null;
        _parentTransform = null;
    }
}