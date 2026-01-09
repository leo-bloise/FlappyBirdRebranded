using FlappyBird.Entities;
using FlappyBird.Lib;
using Microsoft.Xna.Framework;
using System;

namespace FlappyBird.Generators;

public class PipeGenerator
{
    private Rectangle Bounds { get; set; }

    private Random _random;

    private int _spaceBetweenPipes;

    private TextureRegion _region;

    public PipeGenerator(Rectangle bounds, int spaceBetweenPipes, TextureRegion textureRegion)
    {
        Bounds = bounds;
        _spaceBetweenPipes = spaceBetweenPipes;
        _random = new Random();
        _region = textureRegion;
    }

    private int GeneratePositionForUpPipe(Rectangle destinyRectangle)
    {
        return _random.Next(0, destinyRectangle.Height);
    }

    private Vector2 GeneratePositionForDownPipe(Vector2 upPipePosition)
    {
        return new Vector2(upPipePosition.X, upPipePosition.Y + _spaceBetweenPipes);
    }

    public Pipes Generate()
    {
        Rectangle destinyRectangle = new Rectangle(
            Bounds.X, 
            Bounds.Y, 
            Bounds.Width, 
            Bounds.Height / 2
        );
        
        Vector2 pipeUpPosition = new Vector2(
            destinyRectangle.Width,
            GeneratePositionForUpPipe(destinyRectangle)
        );

        Vector2 pipeDownPosition = GeneratePositionForDownPipe(pipeUpPosition);

        return new Pipes(_region, pipeDownPosition, pipeUpPosition);
    }
}
