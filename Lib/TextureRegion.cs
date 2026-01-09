using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Lib;

public class TextureRegion
{
    public Texture2D Texture { get; private set; }
    
    public Rectangle SourceRectangle { get; private set; }
    
    public int Width { get => SourceRectangle.Width; }
    
    public int Height { get => SourceRectangle.Height; }

    public Vector2 Center { get => new Vector2(Width, Height) * 0.5f; }

    public TextureRegion(Texture2D texture, int x, int y, int width, int height)
    {
        Texture = texture;
        SourceRectangle = new Rectangle(x, y, width, height);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects, float layerDepth)
    {
        spriteBatch.Draw(
            Texture,
            position,
            SourceRectangle,
            color,
            rotation,
            origin,
            scale,
            spriteEffects,
            layerDepth
            );
    }
}