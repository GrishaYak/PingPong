using System;
using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Forms.Controls;
using Gum.Forms.DefaultVisuals;
using Gum.Graphics.Animation;
using Gum.Managers;
using Microsoft.Xna.Framework.Input;
using MonoGameGum.GueDeriving;
using MonoGameLib.Graphics;
using static MonoGameLib.Text.TextInst;

namespace PingPong.UI;


public class MyButton : Button
{
    /// <summary>
    /// Creates a custom button visual.
    /// </summary>
    /// <param name="atlas">TextureAtlas with button's textures</param>
    /// <param name="text">Text on the button</param>
    /// <param name="unfocused">Name of the TextureRegion with unfocused button texture</param>
    /// <param name="focused">Name of the TextureRegion with focused button texture</param>
    public MyButton(TextureAtlas atlas, string fontFile, string text="Replace Me", string unfocused="unfocused_btn", string focused="focused_btn")
    {
        var buttonVisual = (ButtonVisual)Visual;
        buttonVisual.Height = 14f;
        buttonVisual.HeightUnits = DimensionUnitType.Absolute;
        buttonVisual.Width = 21f;
        buttonVisual.WidthUnits = DimensionUnitType.RelativeToChildren;


        NineSliceRuntime background = buttonVisual.Background;
        background.Texture = atlas.Texture;
        background.TextureAddress = TextureAddress.Custom;
        background.Color = Microsoft.Xna.Framework.Color.White;

        CreateTextInstance(buttonVisual.TextInstance, text, fontFile:fontFile);

        var unfocusedTextureRegion = atlas.GetRegion(unfocused);
        var unfocusedAnimation = new AnimationChain();
        unfocusedAnimation.Name = nameof(unfocusedAnimation);
        var unfocusedFrame = new AnimationFrame
        {
            TopCoordinate = unfocusedTextureRegion.TopTextureCoordinate,
            BottomCoordinate = unfocusedTextureRegion.BottomTextureCoordinate,
            LeftCoordinate = unfocusedTextureRegion.LeftTextureCoordinate,
            RightCoordinate = unfocusedTextureRegion.RightTextureCoordinate,
            FrameLength = 1.2f,
            Texture = atlas.Texture // same as unfocusedTextureRegion.Texture
        };
        unfocusedAnimation.Add(unfocusedFrame);

        var focusedTextureRegion = atlas.GetRegion(focused);
        var focusedAnimation = new AnimationChain();
        unfocusedAnimation.Name = nameof(focusedAnimation);
        var focusedFrame = new AnimationFrame
        {
            TopCoordinate = focusedTextureRegion.TopTextureCoordinate,
            BottomCoordinate = focusedTextureRegion.BottomTextureCoordinate,
            LeftCoordinate = focusedTextureRegion.LeftTextureCoordinate,
            RightCoordinate = focusedTextureRegion.RightTextureCoordinate,
            FrameLength = 1.2f,
            Texture = atlas.Texture
        };
        focusedAnimation.Add(focusedFrame);

        background.AnimationChains = new AnimationChainList {unfocusedAnimation, focusedAnimation};
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
