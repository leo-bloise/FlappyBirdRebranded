using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace FlappyBird.Lib;

public class Animation
{
    private readonly List<TextureRegion> _frames;

    public ImmutableArray<TextureRegion> Frames { get { return _frames.ToImmutableArray(); } }

    public TimeSpan Delay { get; private set; }

    public Animation()
    {
        _frames = new List<TextureRegion>();
        Delay = TimeSpan.FromMilliseconds(100);
    }

    public Animation(List<TextureRegion> frames, TimeSpan delay)
    {
        _frames = frames;
        Delay = delay;
    }
}
