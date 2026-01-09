using FlappyBird.Entities;
using FlappyBird.Generators;
using FlappyBird.Lib;
using FlappyBird.Lib.Drawing;
using FlappyBird.Lib.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace FlappyBird;

public class MainGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private TextureRegion _background;
    private Base _base;
    private AnimatedSprite _bird;
    private List<Pipes> _pipes;
    private PipeGenerator _pipeGenerator;
    private TimeSpan _elapsedTime = TimeSpan.Zero;

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
        

        _background = atlas.GetRegion("background");
        _bird = new Bird(atlas.CreateAnimatedSprite("bird").Animation);
        _base = new Base(atlas.GetRegion("base"), Window.ClientBounds);
        _bird.Position = GetRectangleCenter(Window.ClientBounds);
        
        var pipeRegion = atlas.GetRegion("pipe");

        _pipeGenerator = new PipeGenerator(
            Window.ClientBounds, 
            120, 
            pipeRegion
        );

        _pipes = new List<Pipes>();
    }

    protected override void Update(GameTime gameTime)
    {
        InputManager.Update();

        _elapsedTime += gameTime.ElapsedGameTime;

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        _bird.Update(gameTime);
        _base.Update();
        _pipes.ForEach(pipe => pipe.Update());

        if(_elapsedTime >= TimeSpan.FromMilliseconds(5000))
        {
            _pipes.Add(_pipeGenerator.Generate());
            _elapsedTime = TimeSpan.Zero;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        _spriteBatch.Begin();

        DrawHelper.Draw(_spriteBatch, _background);

        _bird.Draw(_spriteBatch);

        _pipes.ForEach(pipe => pipe.Draw(_spriteBatch));

        _base.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}