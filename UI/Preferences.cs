using static PingPong.Window;

namespace PingPong.UI;

public static class Preferences
{
    public static class Font
    {
        public static string FontFile { get; } = "fonts/default.fnt";
        public static float StandardFontScale { get => SymbolScreenHeightScale * 0.055f; }
    }
    public static class UniversalTextures
    {
        public static string Transparent { get => "transparent"; }
    }

    public static class Button
    {
        public static float Width { get; } = BufferWidth * 0.15f;
        public static float Height { get; } = BufferHeight * 0.095f;
        public static float TextIndent { get; } = BufferMultiplyer * 4;
        public static string UnfocusedRegion { get; } = "unfocused_btn";
        public static string FocusedRegion { get; } = "focused_btn";
        public static string FontFile { get => Font.FontFile; }
        public static float StandardSizeMultiplyer { get => 1f; }
        public static float StandardFontScale { get => Font.StandardFontScale; }
    }
    public static class SliderWithText
    {
        public static float TotalWidth { get; } = 0.7f * BufferWidth;
        public static float TotalHeight { get; } = 0.35f * BufferHeight;
        public static float TextX { get => 0f; }
        public static float TextY { get => 0f; }
        public static class Slider
        {
            public static float Width {get;} = TotalWidth;
            public static float Height {get => TotalHeight*0.65f;}
            public static float X {get;} = 0f;
            public static float Y {get;} = GetFontSize(FontScale) + 0.01f*BufferHeight;

        }
        public static float FontScale { get; } = Font.StandardFontScale * 1.05f;
        public static string FontFile { get => Font.FontFile; }
    }
}
