using FlappyBird.Entities;
using FlappyBird.Lib;
using FlappyBird.Lib.Drawing;
using FlappyBird.Lib.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FlappyBird;

public class MainGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private TextureRegion _background;
    private Base _base;
    public Bird _bird;
    private PipeManager _pipeManager;
    public bool _pause = false;
    private Score _score;

    public static InputManager InputManager { get; private set; }

    public MainGame()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        Window.AllowUserResizing = false;
        _graphics.IsFullScreen = false;
        _graphics.PreferredBackBufferWidth = 280;
        _graphics.PreferredBackBufferHeight = 512;
        _graphics.ApplyChanges();

        InputManager = new InputManager();

        base.Initialize();
    }

    private Vector2 GetRectangleCenter(Rectangle rectangle)
    {
        return new Vector2(rectangle.Width, rectangle.Height) * 0.5f;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        TextureAtlas atlas = TextureAtlas.FromFile(Content, "Images/atlas-definition.xml");

        #if DEBUG
            Debug.CreateInstance(GraphicsDevice, _spriteBatch);
        #endif

        _background = atlas.GetRegion("background");
        _bird = new Bird(atlas.CreateAnimatedSprite("bird").Animation);
        _base = new Base(atlas.GetRegion("base"), Window.ClientBounds);
        _bird.Position = GetRectangleCenter(Window.ClientBounds);
        _score = new Score(Content.Load<SpriteFont>("Flappy"));
        
        var pipeRegion = atlas.GetRegion("pipe");

        _pipeManager = new PipeManager(Window.ClientBounds, 120, pipeRegion, _bird);

        _pipeManager.OnPipeCollision += () =>
        {
            _pause = true;
        };

        _pipeManager.OnScore += () =>
        {
            if(!_pause)
            {
                _score.Increment();
            }
        };
    }

    protected override void Update(GameTime gameTime)
    {
        InputManager.Update();

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if(_pause)
        {
            base.Update(gameTime);
            return;
        }

        _bird.Update(gameTime);
        _base.Update();
        _pipeManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    { 
        GraphicsDevice.Clear(Color.Black);
        
        _spriteBatch.Begin();

        DrawHelper.Draw(_spriteBatch, _background);

        _bird.Draw(_spriteBatch);

        _pipeManager.DrawPipes(_spriteBatch);

        _base.Draw(_spriteBatch);

        _score.Draw(_spriteBatch, Window.ClientBounds);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}