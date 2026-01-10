namespace PingPong;

public static class Window
{
    public static string WindowName { get; } = "AeroPong";
    public static int ScreenWidth { get; } = 900;
    public static int ScreenHeight { get; } = 500;
    public static bool IsFullScreen { get; } = false;
    public static float BufferMultiplyer { get; } = 3.0f;
    public static int BufferWidth { get => ScreenWidth / (int)BufferMultiplyer; }
    public static int BufferHeight { get => ScreenHeight / (int)BufferMultiplyer; }

    /// <summary>
    /// A constant you have to multiply on to make a symbol have a height of a screen
    /// </summary>
    public static float SymbolScreenHeightScale { get; } = BufferHeight / 26;

    private static readonly float ratio = BufferHeight / SymbolScreenHeightScale;

    /// <summary>
    /// Returns size of a letter in pixels in buffer.
    /// </summary>
    /// <param name="fontScale"></param>
    /// <returns></returns>
    public static float GetFontSize(float fontScale)
    {
        return fontScale * ratio;
    }

}
