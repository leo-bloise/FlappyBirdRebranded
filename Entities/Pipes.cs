using FlappyBird.Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Entities;

public class Pipes
{
    private TextureRegion _region;
    
    private Vector2 _downPipePosition;
    
    private Vector2 _upPipePosition;

    public int Width
    {
        get => _region.Width;
    }

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

    public Rectangle UpPipeBoundingBox
    {
        get
        {
            return new Rectangle((int)_upPipePosition.X, 0, _region.Width, (int)(_upPipePosition.Y));
        }
    }

    public Rectangle DownPipeBoundingBox
    {
        get
        {
            return new Rectangle((int)_downPipePosition.X, (int)_downPipePosition.Y, _region.Width, _region.Height);
        }
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

        #if DEBUG
        Debug.Instance.DrawBoundingBox(UpPipeBoundingBox, Color.Blue);
        Debug.Instance.DrawBoundingBox(DownPipeBoundingBox, Color.Red);
        #endif
    }
}
