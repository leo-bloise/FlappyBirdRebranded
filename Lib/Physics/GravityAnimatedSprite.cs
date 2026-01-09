using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird.Lib.Physics;

public class GravityAnimatedSprite : AnimatedSprite
{
    private Vector2 _gravity = new Vector2(0, 800f);

    protected Vector2 _velocity = new Vector2(0, 0);

    private Vector2 _terminalVelocity = new Vector2(0, 500f);

    public GravityAnimatedSprite(Animation animation) : base(animation)
    {
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
