using FlappyBird.Generators;
using FlappyBird.Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlappyBird.Entities;

public class PipeManager(Rectangle bounds, int spaceBetweenPipes, TextureRegion region, Bird bird)
{
    private Queue<Pipes> _existingPipes = new();

    private PipeGenerator _pipeGenerator = new PipeGenerator(bounds, spaceBetweenPipes, region);

    public event Action OnPipeCollision;

    public int QuantityOfPipes { get => _existingPipes.Count;  }

    private TimeSpan _elpasedTimeBetweenGenerators = TimeSpan.Zero;

    private TimeSpan _intervalBetweenGenerations = TimeSpan.FromMilliseconds(4000);

    private void UpdatePipes()
    {
        foreach (Pipes pipes in _existingPipes)
        {
            pipes.Update(bird);
        }
    }

    private void RemovePipeIfOutsideOfScreen()
    {
        if (QuantityOfPipes <= 0) return;

        Pipes pipe = _existingPipes.First();

        if(pipe.UpPipeBoundingBox.X == pipe.Width * -1)
        {
            _existingPipes.Dequeue().OnBirdColliding -= HandleBirdColliding;
        }
    }

    private void HandleBirdColliding()
    {
        OnPipeCollision.Invoke();
    }

    public void Update(GameTime gameTime)
    {
        _elpasedTimeBetweenGenerators += gameTime.ElapsedGameTime;

        RemovePipeIfOutsideOfScreen();

        UpdatePipes();

        if (_elpasedTimeBetweenGenerators < _intervalBetweenGenerations)
        {
            return;
        }

        var pipe = _pipeGenerator.Generate();

        pipe.OnBirdColliding += HandleBirdColliding;

        _existingPipes.Enqueue(pipe);
        _elpasedTimeBetweenGenerators = _elpasedTimeBetweenGenerators - _intervalBetweenGenerations;

        return;
    }

    public void DrawPipes(SpriteBatch spriteBatch)
    {
        foreach (Pipes pipes in _existingPipes)
        {
            pipes.Draw(spriteBatch);
        }
    }
}
