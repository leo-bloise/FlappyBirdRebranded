using FlappyBird.Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Entities;

public class Pipes
{
    private TextureRegion _region;
    private Vector2 _downPipePosition;
    private Vector2 _upPipePosition;

    public Vector2 CurrentPosition 
    { 
        get
        {
            return new Vector2(_upPipePosition.X, _upPipePosition.Y); 
        }
    }

    public Pipes(TextureRegion region, Vector2 downPipePosition, Vector2 upPipePosition)
    {
        _region = region;
        _downPipePosition = downPipePosition;
        _upPipePosition = upPipePosition;
    }

    public void Update() 
    {
        _downPipePosition.X -= 1;
        _upPipePosition.X -= 1;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        Vector2 origin = new Vector2(_region.Width, _region.Height) * 0.5f;


        _region.Draw(
            spriteBatch, 
            _downPipePosition, 
            Color.White, 
            0f, 
            Vector2.Zero, 
            Vector2.One, 
            SpriteEffects.None,  
            0f 
        );
        _region.Draw(
            spriteBatch, 
            _upPipePosition - new Vector2(0, origin.Y) + new Vector2(origin.X, 0), 
            Color.White, 
            MathHelper.ToRadians(180), 
            origin, 
            Vector2.One, 
            SpriteEffects.None, 0f
        );
    }
}
