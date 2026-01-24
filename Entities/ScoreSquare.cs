using FlappyBird.Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FlappyBird.Entities;

public class ScoreSquare
{
    private static Texture2D? _pixel;

    private readonly SpriteFont _font;

    private readonly Rectangle _bounds;

    private readonly Color _background = Palette.PanelBackground;

    private readonly Color _border = Palette.Accent;

    private readonly Color _text = Palette.Text;

    private readonly Score _score;

    public ScoreSquare(SpriteFont font, Rectangle bounds, Score score)
    {
        _font = font ?? throw new ArgumentNullException(nameof(font));
        _bounds = bounds;
        _score = score;

        if (_pixel == null)
        {
            var device = MainGame.SpriteBatch.GraphicsDevice;
            _pixel = new Texture2D(device, 1, 1);
            _pixel.SetData(new[] { Color.White });
        }

        GameDataService.Instance.AddScore(score.Amount);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_pixel!, _bounds, _background);

        int thickness = 2;
        
        spriteBatch.Draw(_pixel!, new Rectangle(_bounds.X, _bounds.Y, _bounds.Width, thickness), _border);
        
        spriteBatch.Draw(_pixel!, new Rectangle(_bounds.X, _bounds.Bottom - thickness, _bounds.Width, thickness), _border);
        
        spriteBatch.Draw(_pixel!, new Rectangle(_bounds.X, _bounds.Y, thickness, _bounds.Height), _border);
        
        spriteBatch.Draw(_pixel!, new Rectangle(_bounds.Right - thickness, _bounds.Y, thickness, _bounds.Height), _border);


        string scoreLabel = "SCORE";
        string scoreValue = _score.Amount.ToString();

        string bestLabel = "BEST";
        string bestValue = GameDataService.Instance.GetHighScore().ToString();

        // Measure
        var scoreLabelSize = _font.MeasureString(scoreLabel);
        var scoreValueSize = _font.MeasureString(scoreValue);

        var bestLabelSize = _font.MeasureString(bestLabel);
        var bestValueSize = _font.MeasureString(bestValue);

        // Vertical layout positions
        float topSectionY = _bounds.Y + _bounds.Height * 0.20f;
        float bottomSectionY = _bounds.Y + _bounds.Height * 0.55f;

        // SCORE label
        var scoreLabelPos = new Vector2(
            _bounds.X + (_bounds.Width - scoreLabelSize.X) * 0.5f,
            topSectionY
        );

        // SCORE value
        var scoreValuePos = new Vector2(
            _bounds.X + (_bounds.Width - scoreValueSize.X) * 0.5f,
            topSectionY + scoreLabelSize.Y + 4
        );

        // BEST label
        var bestLabelPos = new Vector2(
            _bounds.X + (_bounds.Width - bestLabelSize.X) * 0.5f,
            bottomSectionY
        );

        // BEST value
        var bestValuePos = new Vector2(
            _bounds.X + (_bounds.Width - bestValueSize.X) * 0.5f,
            bottomSectionY + bestLabelSize.Y + 4
        );

        // Draw
        spriteBatch.DrawString(_font, scoreLabel, scoreLabelPos, _text);
        spriteBatch.DrawString(_font, scoreValue, scoreValuePos, _text);

        spriteBatch.DrawString(_font, bestLabel, bestLabelPos, _text);
        spriteBatch.DrawString(_font, bestValue, bestValuePos, _text);
    }
}
