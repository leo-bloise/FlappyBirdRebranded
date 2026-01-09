using FlappyBird.Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Entities;

public class Base
{
    private TextureRegion _sprite;

    private Rectangle _destinationRectangle;

    private Vector2 _position1;

    private Vector2 _position2;
    
    public Base(TextureRegion region, Rectangle destinationRectangle) 
    {
        _sprite = region;
        _destinationRectangle = destinationRectangle;
        _initialize(); 
    }

    private void _initialize()
    {
        _position1 = new Vector2(0, _destinationRectangle.Height - _sprite.Height);
        _position2 = new Vector2(_sprite.Width - 1, _position1.Y);
    }

    public void Update()
    {
        _position1.X -= 1;
        _position2.X -= 1;
        
        if(_position1.X <= -_sprite.Width)
        {
            _position1.X = _position2.X + _sprite.Width - 1;
        }

        if(_position2.X <= _sprite.Width * -1)
        {
            _position2.X = _position1.X + _sprite.Width - 1;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch, _position1, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        _sprite.Draw(spriteBatch, _position2, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
    }

}
