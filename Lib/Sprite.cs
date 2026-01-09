using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Lib;

public class Sprite
{
    public TextureRegion Region { get; protected set; }

    public Color Color { get; set; } = Color.White;

    public float Rotation { get; set; } = 0.0f;

    public Vector2 Scale { get; set; } = new Vector2(1f, 1f);

    public Vector2 Origin { get; set; } = Vector2.Zero;

    public SpriteEffects Effects { get; set; } = SpriteEffects.None;

    public float LayerDepth { get; set; } = 0.0f;

    public Vector2 Position { get; set;  } = Vector2.Zero;

    public float Width => Region.Width * Scale.X;

    public float Height => Region.Height * Scale.Y;

    public Sprite() { }

    public Sprite(TextureRegion region)
    {
        Region = region;
    }

    public void CenterOrigin()
    {
        Origin = new Vector2(Region.Width, Region.Height) * 0.5f;
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Region.Draw(
            spriteBatch,
            Position,
            Color,
            Rotation,
            Origin,
            Scale,
            Effects,
            LayerDepth
            );
    }
}
