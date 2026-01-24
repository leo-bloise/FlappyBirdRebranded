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
    }

    public override Scene Update(GameTime gameTime)
    {
        base.Update(gameTime);
           
        _base.Update();
        _bird.Update(gameTime);

        return null;
    }

    public override void Draw(GameTime gameTime)
    {
        MainGame.SpriteBatch.Begin();

        _background.Draw(MainGame.SpriteBatch, Vector2.Zero, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

        _base.Draw(MainGame.SpriteBatch);

        _bird.Draw(MainGame.SpriteBatch);

        MainGame.SpriteBatch.End();
    }
}
