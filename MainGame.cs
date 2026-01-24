using FlappyBird.Lib;
using FlappyBird.Lib.Input;
using FlappyBird.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;

namespace FlappyBird;

public class MainGame : Game
{
    public static Scene ActiveScene { get; private set; }

    public static Scene NextScene { get; private set; }

    private GraphicsDeviceManager _graphics;

    private static SpriteBatch _spriteBatch;

    private static Rectangle _destinationRectangle;

    public static Rectangle DestinationRectangle { get => _destinationRectangle; }

    public static SpriteBatch SpriteBatch => _spriteBatch;

    public static ContentManager ContentManager { get; private set; }

    public static InputManager InputManager { get; private set; }

    public static AudioThread AudioThread { get; private set; }

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        Window.Title = "Flappy Bird";
        Window.AllowUserResizing = false;

        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 280;
        _graphics.PreferredBackBufferHeight = 512;
        _graphics.ApplyChanges();

        InputManager = new InputManager();

        Content.RootDirectory = "Content";
        ContentManager = base.Content;
        
        _destinationRectangle = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        Window.ClientSizeChanged += (s, e) =>
        {
            _destinationRectangle = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        };

        NextScene = new MainMenu();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        AudioManager.Instance.LoadSong("die", Content.Load<Song>("Sounds/die"));
        AudioManager.Instance.LoadSong("wing", Content.Load<Song>("Sounds/wing"));
        AudioManager.Instance.LoadSong("hit", Content.Load<Song>("Sounds/hit"));
        AudioManager.Instance.LoadSong("point", Content.Load<Song>("Sounds/point"));
        AudioManager.Instance.LoadSong("swoosh", Content.Load<Song>("Sounds/swoosh"));

        AudioThread = new AudioThread();

        AudioThread.Start();
    }

    private void TransitionScene()
    {
        if(ActiveScene != null)
        {
            ActiveScene.Dispose();
        }

        GC.Collect();

        ActiveScene = NextScene;
        
        NextScene = null;

        if(ActiveScene != null)
        {
            ActiveScene.Initialize();
        }
    }

    protected override void Update(GameTime gameTime)
    {
        InputManager.Update();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if(NextScene != null)
        {
             TransitionScene();
        }

        if (ActiveScene != null)
        {
            NextScene = ActiveScene.Update(gameTime);
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        if(ActiveScene == null)
        {
            base.Draw(gameTime);
            return;
        }

        ActiveScene.Draw(gameTime);

        base.Draw(gameTime);
    }
}