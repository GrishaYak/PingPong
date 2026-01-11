using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using static PingPong.Window;

namespace PingPong.UI;

public static class Preferences
{
    public static class Font
    {
        public static string FontFile { get; } = "fonts/default.fnt";
        public static float StandardFontScale { get; } = SymbolScreenHeightScale * 0.055f;
    }
    public static class UniversalTextures
    {
        public static string Transparent { get; } = "transparent";
    }
    public static class Colors
    {
        public static Color FocusedColor { get; } = Color.White * 0.9f;
        public static Color UnfocusedColor { get; } = Color.Gray;
        public static Color DefaultTextColor { get; } = Color.White;
    }
    public static class Button
    {
        public static float Width { get; } = BufferWidth * 0.15f;
        public static float Height { get; } = BufferHeight * 0.095f;
        public static float TextIndent { get; } = BufferMultiplyer * 4;
        public static string UnfocusedRegion { get; } = "unfocused_btn";
        public static string FocusedRegion { get; } = "focused_btn";
        public static float StandardSizeMultiplyer { get; } = 1;
        public static float StandardFontScale { get; } = Font.StandardFontScale;
        public static string FontFile { get => Font.FontFile; }
    }
    public static class SliderWithText
    {
        public static float StandardIndent { get; } = TotalHeight + BufferHeight * 0.05f;
        public static float TotalWidth { get; } = 0.7f * BufferWidth;
        public static float TotalHeight { get; } = 0.35f * BufferHeight;
        public static float TextX { get; } = 0;
        public static float TextY { get; } = 0;
        public static class SliderOnly
        {
            public static float Width { get; } = TotalWidth;
            public static float Height { get; } = TotalHeight * 0.65f;
            public static float X { get; } = 0f;
            public static float Y { get; } = GetFontSize(FontScale) + 0.01f * BufferHeight;

        }
        public static string LeftSliderRegion { get; } = "slider-off";
        public static string MiddleSliderRegion { get; } = "slider-middle";
        public static string RightSliderRegion { get; } = "slider-max";
        public static float FontScale { get; } = Font.StandardFontScale * 1.05f;
        public static string FontFile { get; } = Font.FontFile;
        public static float LeftSliderPartWidth { get; } = BufferWidth * 0.09f;
        public static float RightSliderPartWidth { get; } = LeftSliderPartWidth;
        public static float MiddleSliderPartWidth { get; } = SliderOnly.Width - LeftSliderPartWidth - RightSliderPartWidth;
        /// <summary>
        /// Value of slider in percents.
        /// </summary>
        public static float DefaultSliderValue { get; } = 100f;
    }

    public static class OptionsPanel
    {
        public static class OptionsText
        {
            public static float X {get;} = BufferWidth * 0.05f;
            public static float Y {get;} = BufferHeight * 0.05f;
            public static float FontScale {get;} = Font.StandardFontScale * 2f;

        }
    }
}
