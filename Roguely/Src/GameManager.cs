using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Roguely.Core.Entities;
using Roguely.Rendering;

namespace Roguely;

public class GameManager : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private HashSet<Entity> _entities = new();
    private HashSet<SpriteRenderer> _renderers = new();
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    public GameManager()
    {
        _instance = this;
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Player player = new Player();
        _entities.Add(player);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var entity in _entities)
        {
            if (!entity.Enabled)
                continue;

            // Update each entity
            entity.BaseUpdate((float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        base.Update(gameTime);
    }

    public void RegisterRenderer(SpriteRenderer renderer)
    {
        if (renderer == null || _renderers.Contains(renderer))
            return;

        _renderers.Add(renderer);
    }

    public void UnregisterRenderer(SpriteRenderer renderer)
    {
        if (renderer == null || !_renderers.Contains(renderer))
            return;

        _renderers.Remove(renderer);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        foreach (var renderer in _renderers)
        {
            renderer.Draw(_spriteBatch);
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
