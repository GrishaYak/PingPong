using Microsoft.Xna.Framework;

namespace PingPong;

public static class Window
{
    public const string WindowName  = "AeroPong";
    public const int ScreenWidth  = 900;
    public const int ScreenHeight  = 500;
    public const bool IsFullScreen  = false;
    public const float BufferMultiplyer  = 3.0f;
    public const int BufferWidth  = ScreenWidth / (int)BufferMultiplyer; 
    public const int BufferHeight = ScreenHeight / (int)BufferMultiplyer; 

    /// <summary>
    /// A constant you have to multiply on to make a symbol have a height of a screen
    /// </summary>
    public const float SymbolScreenHeightScale  = BufferHeight / 26;

    public const float FontRatio = BufferHeight / SymbolScreenHeightScale;

    /// <summary>
    /// Returns height of a letter in pixels. (simply multiplies given fontScale with FontRatio)
    /// </summary>
    /// <param name="fontScale"></param>
    /// <returns></returns>
    public static float GetFontSize(float fontScale)
    {
        return fontScale * FontRatio;
    }
    public static Vector2 Screen = new Vector2(ScreenWidth, ScreenHeight) * 0.5f;
}
