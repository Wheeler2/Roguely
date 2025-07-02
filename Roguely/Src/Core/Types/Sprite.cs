using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roguely.Core;

public struct Sprite
{
    public Sprite(Texture2D texture, Color color = default, Vector2 origin = default)
    {
        Texture = texture ?? throw new ArgumentNullException(nameof(texture), "Texture cannot be null");
        Color = color == default ? Color.White : color;
        Origin = origin;
    }

    public Texture2D Texture { get; set; }
    public Color Color { get; set; } = Color.White;
    public Vector2 Origin { get; set; } = Vector2.Zero;
}