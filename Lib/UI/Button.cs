using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace FlappyBird.Lib.UI;

/// <summary>
/// Simple button component that draws a rectangle with centered text and exposes an OnClick event.
/// Call Update() every frame and Draw() between SpriteBatch.Begin()/End().
/// </summary>
public class Button
{
    private readonly SpriteFont _font;
    private readonly Rectangle _bounds;
    private readonly Texture2D _pixel;

    public string Text { get; set; }

    public Color Background { get; set; } = Palette.ButtonBackground;
    public Color HoverBackground { get; set; } = Palette.ButtonHover;
    public Color BorderColor { get; set; } = Palette.Accent;
    public Color TextColor { get; set; } = Palette.Text;

    public event Action? OnClick;

    private bool _isHovering;
    private bool _wasPressed;

    public Button(SpriteFont font, Rectangle bounds, string text)
    {
        _font = font ?? throw new ArgumentNullException(nameof(font));
        _bounds = bounds;
        Text = text ?? string.Empty;

        var device = MainGame.SpriteBatch.GraphicsDevice;
        _pixel = new Texture2D(device, 1, 1);
        _pixel.SetData(new[] { Color.White });
    }

    public void Update()
    {
        var mouse = Mouse.GetState();
        var point = new Point(mouse.X, mouse.Y);
        _isHovering = _bounds.Contains(point);

        var leftPressed = mouse.LeftButton == ButtonState.Pressed;

        if (_isHovering && leftPressed && !_wasPressed)
        {
            _wasPressed = true;
        }
        else if (_wasPressed && !leftPressed)
        {
            if (_isHovering)
            {
                OnClick?.Invoke();
            }

            _wasPressed = false;
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        var bg = _isHovering ? HoverBackground : Background;

        // background
        spriteBatch.Draw(_pixel, _bounds, bg);

        // border
        var thickness = 2;
        spriteBatch.Draw(_pixel, new Rectangle(_bounds.X, _bounds.Y, _bounds.Width, thickness), BorderColor);
        spriteBatch.Draw(_pixel, new Rectangle(_bounds.X, _bounds.Bottom - thickness, _bounds.Width, thickness), BorderColor);
        spriteBatch.Draw(_pixel, new Rectangle(_bounds.X, _bounds.Y, thickness, _bounds.Height), BorderColor);
        spriteBatch.Draw(_pixel, new Rectangle(_bounds.Right - thickness, _bounds.Y, thickness, _bounds.Height), BorderColor);

        // text
        var size = _font.MeasureString(Text);
        var pos = new Vector2(_bounds.X + (_bounds.Width - size.X) * 0.5f, _bounds.Y + (_bounds.Height - size.Y) * 0.5f);
        spriteBatch.DrawString(_font, Text, pos, TextColor);
    }
}
