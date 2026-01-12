using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Entities;

public class Score
{
    public SpriteFont Font { get; private set; }

    public int Amount { get; private set; } = 0;

    public Score(SpriteFont font) {
        Font = font;
    }

    public void Increment()
    {
        Amount++;
    }

    public void Draw(SpriteBatch _spriteBatch, Rectangle destination)
    { 
        Vector2 textMiddlePoint = Font.MeasureString(Amount.ToString()) * 0.5f;

        _spriteBatch.DrawString(Font, Amount.ToString(), new Vector2(destination.Width * 0.5f, destination.Height / 8), Color.White, 0f, textMiddlePoint, new Vector2(2, 2), SpriteEffects.None, 0f);
    }
}
