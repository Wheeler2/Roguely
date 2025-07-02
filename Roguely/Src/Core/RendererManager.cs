using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Roguely.Core.Entities;

namespace Roguely.Core.Rendering;

public static class RendererManager
{
    public static void Initialize(GraphicsDeviceManager graphics)
    {
        _graphics = graphics;

        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();

        _initialized = true;
    }

    public static void InitSpriteBatch(SpriteBatch spriteBatch)
    {
        _spriteBatch = spriteBatch;
        if (!_initialized)
            throw new System.InvalidOperationException("RendererManager must be initialized before initializing SpriteBatch.");
    }

    private static GraphicsDeviceManager _graphics;
    private static SpriteBatch _spriteBatch;
    private static bool _initialized = false;

    private static readonly HashSet<IRenderer> _renderers = new();

    public static GraphicsDevice GraphicsDevice => _graphics.GraphicsDevice;

    public static void RegisterRenderer(IRenderer renderer)
    {
        if (renderer == null || _renderers.Contains(renderer))
            return;

        _renderers.Add(renderer);
    }

    public static void UnregisterRenderer(IRenderer renderer)
    {
        if (renderer == null || !_renderers.Contains(renderer))
            return;

        _renderers.Remove(renderer);
    }

    public static void RenderAll()
    {
        if (!_initialized)
            throw new System.InvalidOperationException("RendererManager must be initialized before rendering.");

        _spriteBatch.Begin(transformMatrix: Camera.Main.ViewMatrix, samplerState: SamplerState.PointClamp);

        foreach (var renderer in _renderers)
        {
            renderer.Draw(_spriteBatch);
        }

        _spriteBatch.End();
    }
}