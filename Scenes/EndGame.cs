using FlappyBird.Entities;
using FlappyBird.Lib;
using FlappyBird.Lib.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Scenes;

public class EndGame : Scene
{
    private TextureAtlas _textureAtlas;

    private TextureRegion _background;

    private TextureRegion _gameOverMessage;

    private Base _base;
    
    private SpriteFont _font;

    private ScoreSquare _scoreSquare;

    private Button _tryAgainButton;

    private Score _score;

    private bool _tryAgain = false;

    public EndGame(Score score)
    {
        _score = score;
    }

    public override void LoadContent()
    {
        base.LoadContent();

        _textureAtlas = TextureAtlas.FromFile(Content, "Images/atlas-definition.xml");

        _background = _textureAtlas.GetRegion("background");

        _base = new Base(_textureAtlas.GetRegion("base"), MainGame.DestinationRectangle);

        Texture2D gameOverMessage = Content.Load<Texture2D>("gameover");

        _gameOverMessage = new TextureRegion(gameOverMessage, 0, 0, gameOverMessage.Width, gameOverMessage.Height);

        int rectWidth = 180;
        int rectHeight = 120;
        int marginTop = 30;
        int rectX = (MainGame.DestinationRectangle.Width - rectWidth) / 2;
        int rectY = (MainGame.DestinationRectangle.Height - rectHeight) / 2 + marginTop;

        _font = Content.Load<SpriteFont>("Flappy");

        CreateScoreSquare(rectX, rectY, rectWidth, rectHeight);
        CreateTryAgainButton(rectX, rectY, rectWidth, rectHeight);
    }

    private void CreateScoreSquare(int rectX, int rectY, int rectWidth, int rectHeight)
    {
        _scoreSquare = new ScoreSquare(_font, new Rectangle(rectX, rectY, rectWidth, rectHeight), _score);
    }

    private void CreateTryAgainButton(int rectX, int rectY, int rectWidth, int rectHeight)
    {
        int buttonWidth = 140;
        int buttonHeight = 36;
        int buttonX = (MainGame.DestinationRectangle.Width - buttonWidth) / 2;
        int buttonY = rectY + rectHeight + 12;

        _tryAgainButton = new Button(_font, new Rectangle(buttonX, buttonY, buttonWidth, buttonHeight), "Try Again");
        _tryAgainButton.OnClick += () => { _tryAgain = true; };
    }

    private Vector2 _gameOverMessagePosition => new Vector2(MainGame.DestinationRectangle.Width / 2, MainGame.DestinationRectangle.Height / 3);

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        MainGame.SpriteBatch.Begin();

        _background.Draw(MainGame.SpriteBatch, Vector2.Zero, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);

        _base.Draw(MainGame.SpriteBatch);

        _gameOverMessage.Draw(
            MainGame.SpriteBatch,
            _gameOverMessagePosition, 
            Color.White, 
            0f, 
            _gameOverMessage.Center, 
            Vector2.One, 
            SpriteEffects.None, 
            0f
         );

        _scoreSquare.Draw(MainGame.SpriteBatch);

        _tryAgainButton.Draw(MainGame.SpriteBatch);

        MainGame.SpriteBatch.End();
    }

    public override Scene Update(GameTime gameTime)
    {
        base.Update(gameTime);
        
        _tryAgainButton.Update();

        if(_tryAgain)
        {
            return new MainMenu();
        }

        return null;
    }
}
