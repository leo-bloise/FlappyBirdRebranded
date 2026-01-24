using FlappyBird.Entities;
using FlappyBird.Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace FlappyBird.Scenes;

public class MainMenu : Scene
{
    private TextureAtlas _sceneAtlas;
    
    private TextureRegion _background;

    private TextureRegion _message;

    private Base _base;

    private Bird _bird;

    public override void UnloadContent()
    {
        base.UnloadContent();

        _base = null;
        _bird = null;
        _sceneAtlas = null;
        _background = null;
        _message = null;

        GC.Collect();
    }

    public override void LoadContent()
    {
        base.LoadContent();

        _sceneAtlas = TextureAtlas.FromFile(Content, Path.Combine("Images", "atlas-definition.xml"));       

        _background = _sceneAtlas.GetRegion("background");

        _base = new Base(_sceneAtlas.GetRegion("base"), MainGame.DestinationRectangle);

        Texture2D texture = Content.Load<Texture2D>(Path.Combine("message"));

        _message = new TextureRegion(texture, 0, 0, texture.Width, texture.Height);

        _bird = new Bird(_sceneAtlas.GetAnimation("bird"), Vector2.Zero);

        _bird.Position = CenterRectDestination();
    }

    private Vector2 CenterRectDestination()
    {
        return new Vector2(
            MainGame.DestinationRectangle.Width,
            MainGame.DestinationRectangle.Height
        ) * 0.5f;
    }

    private Vector2 CenterMessage()
    {
        return new Vector2(MainGame.DestinationRectangle.Width / 2, MainGame.DestinationRectangle.Height / 3);
    }

    public override Scene Update(GameTime gameTime)
    {
        base.Update(gameTime);

        _base.Update();
        _bird.Update(gameTime);

        if (MainGame.InputManager.KeyboardInfo.WasKeyJustPressed(Keys.Space))
        {
            return new Gameplay(_bird);
        }

        return null;
    }

    public override void Draw(GameTime gameTime)
    {
        MainGame.SpriteBatch.Begin();

        _background.Draw(MainGame.SpriteBatch, Vector2.Zero, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        _base.Draw(MainGame.SpriteBatch);
        _message.Draw(MainGame.SpriteBatch, CenterMessage(), Color.White, 0f, new Vector2(_message.Width / 2, _message.Height / 2), Vector2.One, SpriteEffects.None, 0f);
        _bird.Draw(MainGame.SpriteBatch);

        MainGame.SpriteBatch.End();
    }
}
