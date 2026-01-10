using FlappyBird.Generators;
using FlappyBird.Lib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FlappyBird.Entities;

public class PipeManager(Rectangle bounds, int spaceBetweenPipes, TextureRegion region)
{
    private Queue<Pipes> _existingPipes = new();

    private PipeGenerator _pipeGenerator = new PipeGenerator(bounds, spaceBetweenPipes, region);

    public int QuantityOfPipes { get => _existingPipes.Count;  }

    private TimeSpan _elpasedTimeBetweenGenerators = TimeSpan.Zero;

    private TimeSpan _intervalBetweenGenerations = TimeSpan.FromMilliseconds(4000);

    private void UpdatePipes()
    {
        foreach (Pipes pipes in _existingPipes)
        {
            pipes.Update();
        }
    }

    private void RemovePipeIfOutsideOfScreen()
    {
        if (QuantityOfPipes <= 0) return;

        Pipes pipe = _existingPipes.First();

        if(pipe.UpPipeBoundingBox.X == pipe.Width * -1)
        {
            _existingPipes.Dequeue();
        }
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

        _existingPipes.Enqueue(_pipeGenerator.Generate());
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
