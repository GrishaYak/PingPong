using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Gum.Content.AnimationChain;
using Gum.DataTypes;
using Microsoft.Xna.Framework;
using MonoGameLib.Graphics;
using static PingPong.Window;

namespace PingPong.UI;

public static class Preferences
{
    public static TextureAtlas Atlas;
    public static class Font
    {
        public const string FontFile = "fonts/default.fnt";
        public const float StandardFontScale = SymbolScreenHeightScale * 0.055f;
    }
    public static class UniversalTextures
    {
        public const string Transparent = "transparent";
        public const string GreenOutline = "panel-background";
        public const string RedOutline = "inner-panel-background";
    }
    public static class Colors
    {
        public static Color FocusedColor { get; } = Color.White * 0.9f;
        public static Color UnfocusedColor { get; } = Color.White * 0.6f;
        public static Color DefaultTextColor { get; } = Color.White;
    }
    public static class Button
    {
        public const float Width = BufferWidth * 0.15f;
        public const float Height = BufferHeight * 0.095f;
        public const float TextIndent = BufferMultiplyer * 4;
        public const string UnfocusedRegion = "unfocused_btn";
        public const string FocusedRegion = "focused_btn";
        public const float StandardSizeMultiplyer = 1;
        public const float StandardFontScale = Font.StandardFontScale;
        public const string FontFile = Font.FontFile;
    }
    public static class SliderWithText
    {
        /// <summary>
        /// Standard distance from top left angle of one slider to another.
        /// </summary>
        public const float StandardIndent = TotalHeight + BufferHeight * 0.04f;
        public const float TotalX = Settings.OptionsText.X;
        public const float TotalWidth = 0.9f * BufferWidth;
        public const float TotalHeight = FontScale * FontRatio + SliderOnly.Height;
        public const float TextX = 0;
        public const float TextY = 0;
        public const float AdditionalTrackHeight = 6f;
        public static class SliderOnly
        {
            public const float Width = TotalWidth;
            public const float Height = BufferHeight * 0.05f;
            public const float X = 0f;
            public const float Y = FontScale * FontRatio + 0.01f * BufferHeight;

        }
        public static class TrackInstance
        {
            public const float Width = -2f*BufferMultiplyer;
            public const DimensionUnitType WidthUnits = DimensionUnitType.RelativeToParent;
            public const float Height =  -2 * BufferMultiplyer;
            public const DimensionUnitType HeightUnits = DimensionUnitType.RelativeToParent;
        }
        public const string LeftSliderRegion = "slider-off";
        public const string MiddleSliderRegion = "slider-middle";
        public const string RightSliderRegion = "slider-max";
        public const float FontScale = Font.StandardFontScale * 1.05f;
        public const string FontFile = Font.FontFile;
        public const float LeftSliderPartWidth = BufferWidth * 0.01f;
        public const float RightSliderPartWidth = LeftSliderPartWidth;
        public const float MiddleSliderPartWidth = SliderOnly.Width - LeftSliderPartWidth - RightSliderPartWidth;
        /// <summary>
        /// Value of slider in percents.
        /// </summary>
        public const float DefaultSliderValue = 100f;
        public const double SmallChange = .05;
        public const double LargeChange = .1;
    }

    public static class Settings
    {
        public static class OptionsText
        {
            public const float X = BufferWidth * 0.05f;
            public const float Y = BufferHeight * 0.05f;
            public const float FontScale = Font.StandardFontScale * 2f;
            public static Color Color = Color.White * 0.95f;

        }
        public static class BackButton
        {
            public const float X = -0.02f * BufferWidth;
            public const float Y = X;
        }
        public static class DiscardButton
        {
            public const float X = BackButton.X - Button.Width - 0.03f*BufferWidth;
            public const float Y = BackButton.Y;
        }
        public static class SaveButton
        {
            public const float X = DiscardButton.X - Button.Width - 0.03f*BufferWidth;
            public const float Y = BackButton.Y;
        }
        public const float FirstSliderY = OptionsText.Y + OptionsText.FontScale * FontRatio + 0.06f * BufferHeight;
        public const string PathToSettings = "settings.json";
    }
}
