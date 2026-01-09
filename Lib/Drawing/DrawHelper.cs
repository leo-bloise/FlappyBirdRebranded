using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Lib.Drawing;

public class DrawHelper
{
    public static void DrawRegionInTheGround(SpriteBatch spriteBatch, TextureRegion region, Rectangle destination)
    {
        Vector2 position = new Vector2(0, destination.Height - region.Height);

        region.Draw(spriteBatch, position, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
    }

    public static void Draw(SpriteBatch spriteBatch, TextureRegion region)
    {
        region.Draw(spriteBatch, Vector2.Zero, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
    }
}
