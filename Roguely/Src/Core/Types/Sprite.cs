using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Roguely.Rendering;

public struct Sprite
{
    public Sprite(Texture2D texture, Color color = default)
    {
        Texture = texture;
        Color = color == default ? Color.White : color;
    }

    public Texture2D Texture { get; set; }
    public Color Color { get; set; } = Color.White;
}