using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlappyBird.Lib;

public class AnimatedSprite: Sprite
{
    private int _currentFrame = 0;
    private TimeSpan _elapsed;
    private Animation _animation;

    public Animation Animation
    {
        get { return _animation; } 
        set
        {
            _animation = value;
            Region = _animation.Frames[0];
        }
    }

    public AnimatedSprite(Animation animation): base()
    {
        Animation = animation;
    }

    public virtual void Update(GameTime gameTime)
    {
        _elapsed += gameTime.ElapsedGameTime;

        if(_elapsed >= _animation.Delay)
        {
            _elapsed -= _animation.Delay;
            _currentFrame++;

            if(_currentFrame >= _animation.Frames.Count())
            {
                _currentFrame = 0;
            }

            Region = _animation.Frames[_currentFrame];
        }
    }
}
