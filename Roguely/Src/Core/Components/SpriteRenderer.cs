using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Roguely.Core.Rendering;

namespace Roguely.Core.Components;

public class SpriteRenderer : IComponent, IRenderer
{
    public SpriteRenderer(Sprite sprite, Vector2 position)
    {
        Sprite = sprite;
        _position = position;
    }

    public SpriteRenderer(Sprite sprite, Transform parentTransform = null)
    {
        Sprite = sprite;
        _parentTransform = parentTransform;

        if (_parentTransform != null)
            _position = _parentTransform.Position;
    }

    public Sprite Sprite { get; set; }

    private Entity _parentEntity;
    private Transform _parentTransform;
    private Vector2 _position;
    private float _layerDepth = 0f;

    public void Init()
    {
        if (_parentTransform != null)
        {
            _position = _parentTransform.Position;
        }

        RendererManager.RegisterRenderer(this);
    }

    public void Update()
    {
        if (_parentTransform != null)
        {
            _position = _parentTransform.Position;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if (Sprite.Texture == null)
            return;

        if (_parentTransform != null)
        {
            spriteBatch.Draw(Sprite.Texture, _position, null, Sprite.Color, _parentTransform.Rotation, Sprite.Origin, _parentTransform.Scale, SpriteEffects.None, _layerDepth);
        }
        else
        {
            spriteBatch.Draw(Sprite.Texture, _position, null, Sprite.Color, 0f, origin: Sprite.Origin, Vector2.One, SpriteEffects.None, _layerDepth);
        }
    }

    public void SetParentEntity(Entity entity)
    {
        _parentEntity = entity;

        if (_parentEntity.TryGetComponent(out Transform transform))
        {
            _parentTransform = transform;
            _position = transform.Position;
        }
    }

    public void Destroy()
    {
        RendererManager.UnregisterRenderer(this);
        _parentTransform = null;
        _parentEntity = null;
    }
}