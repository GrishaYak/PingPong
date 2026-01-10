using System;
using System.Buffers;
using System.Collections.Specialized;
using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Forms.Controls;
using Gum.Forms.DefaultVisuals;
using Gum.Graphics.Animation;
using Gum.Managers;
using Microsoft.Xna.Framework.Input;
using MonoGameGum.GueDeriving;
using MonoGameLib.Graphics;
using static MonoGameLib.Text.TextInstance;
using static PingPong.UI.Preferences.Button;

namespace PingPong.UI;


public class MyButton : Button
{
    /// <summary>
    /// A scale that will be used on text when initializing new button 
    /// </summary>
    public static float TextScale { get; set; } = StandardFontScale;
    public static float SizeMultiplyer { get; set; } = StandardSizeMultiplyer;
    private readonly float curSizeMultiplyer;

    private readonly ButtonVisual buttonVisual;
    private readonly string buttonText = "Replace Me";
    private readonly float textScale;
    private readonly string fontFile;
    /// <summary>
    /// Creates a custom button visual.
    /// </summary>
    /// <param name="atlas">TextureAtlas with button's textures</param>
    /// <param name="text">Text on the button</param>
    /// <param name="unfocused">Name of the TextureRegion with unfocused button texture</param>
    /// <param name="focused">Name of the TextureRegion with focused button texture</param>
    public MyButton(TextureAtlas atlas, string customFontFile = null, string text = "Replace Me", float? customTextScale = null, float? sizeMultiplyer = null)
    {
        textScale = customTextScale ?? TextScale;
        fontFile = customFontFile ?? FontFile;
        buttonText = text;
        curSizeMultiplyer = sizeMultiplyer ?? SizeMultiplyer;
        buttonVisual = (ButtonVisual)Visual;
        buttonVisual.Height = Preferences.Button.Height * curSizeMultiplyer;
        buttonVisual.HeightUnits = DimensionUnitType.Absolute;
        VisualWidth = Preferences.Button.Width;
        buttonVisual.WidthUnits = DimensionUnitType.Absolute;

        NineSliceRuntime background = buttonVisual.Background;
        background.Texture = atlas.Texture;
        background.TextureAddress = TextureAddress.Custom;
        background.Color = Microsoft.Xna.Framework.Color.White;

        var unfocusedTextureRegion = atlas.GetRegion(UnfocusedRegion);
        var unfocusedAnimation = new AnimationChain();
        unfocusedAnimation.Name = nameof(unfocusedAnimation);
        var unfocusedFrame = new AnimationFrame
        {
            TopCoordinate = unfocusedTextureRegion.TopTextureCoordinate,
            BottomCoordinate = unfocusedTextureRegion.BottomTextureCoordinate,
            LeftCoordinate = unfocusedTextureRegion.LeftTextureCoordinate,
            RightCoordinate = unfocusedTextureRegion.RightTextureCoordinate,
            FrameLength = 1f,
            Texture = atlas.Texture // same as unfocusedTextureRegion.Texture
        };
        unfocusedAnimation.Add(unfocusedFrame);

        var focusedTextureRegion = atlas.GetRegion(FocusedRegion);
        var focusedAnimation = new AnimationChain();
        unfocusedAnimation.Name = nameof(focusedAnimation);
        var focusedFrame = new AnimationFrame
        {
            TopCoordinate = focusedTextureRegion.TopTextureCoordinate,
            BottomCoordinate = focusedTextureRegion.BottomTextureCoordinate,
            LeftCoordinate = focusedTextureRegion.LeftTextureCoordinate,
            RightCoordinate = focusedTextureRegion.RightTextureCoordinate,
            FrameLength = 1f,
            Texture = atlas.Texture
        };
        focusedAnimation.Add(focusedFrame);

        background.AnimationChains = new AnimationChainList { unfocusedAnimation, focusedAnimation };
        buttonVisual.ButtonCategory.ResetAllStates();

        StateSave enabledState = buttonVisual.States.Enabled;
        enabledState.Apply = () => { background.CurrentChainName = unfocusedAnimation.Name; };

        StateSave focusedState = buttonVisual.States.Focused;
        focusedState.Apply = () => { background.CurrentChainName = focusedAnimation.Name; };

        StateSave highlightedFocused = buttonVisual.States.HighlightedFocused;
        highlightedFocused.Apply = focusedState.Apply;

        StateSave highlighted = buttonVisual.States.Highlighted;
        highlighted.Apply = enabledState.Apply;

        KeyDown += HandleKeyDown;

        buttonVisual.RollOn += HandleRollOn;
    }

    public float VisualWidth
    {
        get => buttonVisual.Width; set
        {
            SetProperties(buttonVisual.TextInstance, fontFile: fontFile);
            float textWidth = buttonVisual.TextInstance.BitmapFont.MeasureString(buttonText) * textScale;
            float ratio = 1;
            if (textWidth > value - TextIndent)
            {
                ratio = (value - TextIndent) / textWidth;
            }

            buttonVisual.Width = value * curSizeMultiplyer;

            SetProperties(buttonVisual.TextInstance, buttonText, fontFile: fontFile, scale: textScale * ratio * curSizeMultiplyer);
        }
    }

    /// <summary>
    /// Handles keyboard input for navigation between buttons using left/right keys.
    /// </summary>
    private void HandleKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Keys.Left)
        {
            // Left arrow navigates to previous control
            HandleTab(TabDirection.Up, loop: true);
        }
        if (e.Key == Keys.Right)
        {
            // Right arrow navigates to next control
            HandleTab(TabDirection.Down, loop: true);
        }
    }

    /// <summary>
    /// Automatically focuses the button when the mouse hovers over it.
    /// </summary>
    private void HandleRollOn(object sender, EventArgs e)
    {
        IsFocused = true;
    }
}
