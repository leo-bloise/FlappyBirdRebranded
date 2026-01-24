using Microsoft.Xna.Framework;

namespace FlappyBird.Lib;

/// <summary>
/// Central color palette used across UI to match Flappy Bird style.
/// Adjust values here to tweak the look globally.
/// </summary>
public static class Palette
{
    // Main UI panel background (soft warm tone)
    public static readonly Color PanelBackground = new Color(255, 245, 204);

    // Panel / button border / accent (golden)
    public static readonly Color Accent = new Color(207, 174, 23);

    // Button background (warm yellow)
    public static readonly Color ButtonBackground = new Color(248, 196, 0);

    // Button hover (slightly darker)
    public static readonly Color ButtonHover = new Color(228, 176, 0);

    // Primary text color used on panels and buttons
    public static readonly Color Text = new Color(34, 34, 34);
}
