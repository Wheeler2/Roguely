using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Roguely.Core;
using Roguely.Core.Input;
using Roguely.Entities;
using Roguely.Core.Entities;
using Roguely.Core.Rendering;

namespace Roguely;

public class GameManager : Game
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public GameManager()
    {
        _instance = this;
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        RendererManager.Initialize(_graphics);

        base.Initialize();
    }

    protected override void BeginRun()
    {
        new Player();
        new TestEntity(new Vector2(100, 100));

        base.BeginRun();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // Initialize the renderer manager
        RendererManager.InitSpriteBatch(_spriteBatch);
    }

    protected override void Update(GameTime gameTime)
    {
        // Update time manager first due to it providing the frame time delta (DeltaTime) for other systems
        Time.Update(gameTime);
        // Update input before processing entities
        InputManager.Update();
        // Update entities
        EntityManager.UpdateEntities();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        RendererManager.RenderAll();

        base.Draw(gameTime);
    }
}
