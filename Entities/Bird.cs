using FlappyBird.Lib;
using FlappyBird.Lib.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace FlappyBird.Entities;

public class Bird : GravityAnimatedSprite
{
    public Rectangle BoundingBox
    {
        get {
            return new Rectangle((int)(Position.X - Origin.X), (int) (Position.Y - Origin.Y), Region.Width -2, Region.Height -2);
        }
    }

    public Bird(Animation animation) : base(animation)
    {
        CenterOrigin();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        if(MainGame.InputManager.KeyboardInfo.WasKeyJustPressed(Microsoft.Xna.Framework.Input.Keys.Space))
        {
            AudioManager.Instance.Play("wing");
            _velocity = new Vector2(_velocity.X, -350f);
            base.Update(gameTime);
        }
    }

    private void CalculateRotationBasedOnVelocity()
    {
        Rotation = MathHelper.Clamp(MathHelper.ToRadians(_velocity.Y / 5), MathHelper.ToRadians(-50), MathHelper.ToRadians(50));
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        CalculateRotationBasedOnVelocity();
        
        base.Draw(spriteBatch);

#if DEBUG
        Debug.Instance.DrawBoundingBox(BoundingBox, Color.Yellow);
#endif
    }
}
