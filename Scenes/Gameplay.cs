using FlappyBird.Entities;
using FlappyBird.Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Scenes;

public class Gameplay : Scene
{
    private TextureRegion _background;

    private Base _base;

    private Bird _bird;

    private TextureAtlas _sceneAtlas;

    private PipeManager _pipeManager;

    private Score _score;

    private bool _pause = false;

    public Gameplay(Bird bird)
    {
        bird.RestoreGravity();
        _bird = bird;
    }

    public override void Initialize()
    {
        base.Initialize();

        _sceneAtlas = TextureAtlas.FromFile(Content, "Images/atlas-definition.xml");
        _bird.Animation = _sceneAtlas.GetAnimation("bird");
        _base = new Base(_sceneAtlas.GetRegion("base"), MainGame.DestinationRectangle);
        _background = _sceneAtlas.GetRegion("background");

        _bird.Position = new Vector2(MainGame.DestinationRectangle.Width, MainGame.DestinationRectangle.Height) * 0.5f;
        _pipeManager = new PipeManager(MainGame.DestinationRectangle, 150, _sceneAtlas.GetRegion("pipe"), _bird);

        _score = new Score(Content.Load<SpriteFont>("Flappy"));

        _pipeManager.OnPipeCollision += () =>
        {
            MainGame.AudioThread.Enqueue("hit");
            _pause = true;
        };

        _pipeManager.OnScore += () =>
        {
            _score.Increment();
            MainGame.AudioThread.Enqueue("point");
        };
    }

    public override Scene Update(GameTime gameTime)
    {
        base.Update(gameTime);
          
        if(_pause)
        {
            return null;
        }

        _base.Update();
        _bird.Update(gameTime);
        _pipeManager.Update(gameTime);

        return null;
    }

    public override void Draw(GameTime gameTime)
    {
        MainGame.SpriteBatch.Begin();

        _background.Draw(MainGame.SpriteBatch, Vector2.Zero, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

        _pipeManager.DrawPipes(MainGame.SpriteBatch);

        _base.Draw(MainGame.SpriteBatch);

        _bird.Draw(MainGame.SpriteBatch);

        _score.Draw(MainGame.SpriteBatch, MainGame.DestinationRectangle);

        MainGame.SpriteBatch.End();
    }
}
