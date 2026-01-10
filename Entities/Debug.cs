using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace FlappyBird.Entities;

public class Debug
{
    private static Debug _instance;

    private GraphicsDevice _device;
    private SpriteBatch _spriteBatch;

    private Debug(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
    {
        _device = graphicsDevice;
        _spriteBatch = spriteBatch;
    }

    public static Debug Instance
    {
        get {
            if (_instance == null) throw new Exception("Instance is null");

            return _instance;
        }
    }

    public static void CreateInstance(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
    {
        if (_instance is not null) throw new Exception("Instance already created");

        _instance = new Debug(graphicsDevice, spriteBatch);
    }

    private Texture2D CreateTexture(Color color)
    {
        var texture = new Texture2D(_device, 1, 1);
        texture.SetData([color]);

        return texture;
    }

    public void DrawBoundingBox(Rectangle rectangle, Color color)
    {
        var texture = CreateTexture(color);

        _spriteBatch.Draw(
            texture,
            rectangle,
            color * 0.5f
        );
    }
}
