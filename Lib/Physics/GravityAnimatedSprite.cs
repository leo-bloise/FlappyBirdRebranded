using Microsoft.Xna.Framework;

namespace FlappyBird.Lib.Physics;

public class GravityAnimatedSprite : AnimatedSprite
{
    private Vector2 _gravity = new Vector2(0, 800f);

    protected Vector2 _velocity = new Vector2(0, 0);

    private Vector2 _terminalVelocity = new Vector2(0, 500f);

    public bool IsGravityEnabled { get => _gravity != Vector2.Zero; }

    public GravityAnimatedSprite(Animation animation) : base(animation)
    {
    }

    public GravityAnimatedSprite(Animation animation, Vector2 gravity): base(animation)
    {
        _gravity = gravity;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
        _velocity += _gravity * dt;

        if(_velocity.Y >= _terminalVelocity.Y)
            _velocity = _terminalVelocity;

        Position += _velocity * dt;
    }
}
