using System;
using Gum.DataTypes;
using Gum.DataTypes.Variables;
using Gum.Managers;
using Microsoft.Xna.Framework;
using Gum.Forms.Controls;
using MonoGameGum.GueDeriving;
using MonoGameLib.Graphics;
using static PingPong.UI.Preferences.SliderWithText;
using static MonoGameLib.Text.TextInstance;
using static PingPong.UI.Preferences.UniversalTextures;
using static PingPong.UI.Preferences.Colors;

namespace PingPong.UI;

public class OptionsSlider : Slider
{
    private TextRuntime textInstance;
    private ColoredRectangleRuntime rangeSelect;

    /// <summary>
    /// Gets or sets the text label for this slider.
    /// </summary>
    public string Text
    {
        get => textInstance.Text;
        set => textInstance.Text = value;
    }
    /// <summary>
    /// Creates a new OptionsSlider instance using graphics from the specified texture atlas.
    /// </summary>
    /// <param name="atlas">The texture atlas containing slider graphics.</param>
    /// <param name="text">The text, that will be placed near the slider.</param>
    public OptionsSlider(TextureAtlas atlas, string fontFile = FontFile, string text = "Replace Me", Color? textColor = null)
    {
        Color colorText = textColor ?? DefaultTextColor;

        // Create the top-level container for all visual elements
        var topLevelContainer = new ContainerRuntime();
        topLevelContainer.Height = TotalHeight;
        topLevelContainer.Width = TotalWidth;

        TextureRegion backgroundRegion = atlas.GetRegion(Transparent);

        // Create the background panel that contains everything
        var background = backgroundRegion.GetNineSlice();
        background.Dock(Gum.Wireframe.Dock.Fill);
        topLevelContainer.AddChild(background);

        // Create the title text element
        textInstance = new TextRuntime
        {
            CustomFontFile = fontFile,
            UseCustomFont = true,
            FontScale = FontScale,
            Text = text,
            X = TextX,
            Y = TextY,
            WidthUnits = DimensionUnitType.RelativeToChildren
        };
        topLevelContainer.AddChild(textInstance);

        // Create the container for the slider track and decorative elements
        var innerContainer = new ContainerRuntime
        {
            Height = SliderOnly.Height,
            Width = SliderOnly.Width,
            X = SliderOnly.X,
            Y = SliderOnly.Y
        };

        topLevelContainer.AddChild(innerContainer);

        TextureRegion innerBackgroundRegion = atlas.GetRegion(Transparent);
        var innerBackground = innerBackgroundRegion.GetNineSlice();
        innerBackground.Dock(Gum.Wireframe.Dock.Fill);

        innerContainer.AddChild(innerBackground);

        TextureRegion offBackgroundRegion = atlas.GetRegion(LeftSliderRegion);
        // Create the "OFF" side of the slider (left end)
        var offBackground = new NineSliceRuntime();
        offBackground.Dock(Gum.Wireframe.Dock.Left);
        offBackground.Texture = atlas.Texture;
        offBackground.TextureAddress = TextureAddress.Custom;
        offBackground.TextureHeight = offBackgroundRegion.Height;
        offBackground.TextureLeft = offBackgroundRegion.SourceRectangle.Left;
        offBackground.TextureTop = offBackgroundRegion.SourceRectangle.Top;
        offBackground.TextureWidth = offBackgroundRegion.Width;
        offBackground.Width = LeftSliderPartWidth;
        offBackground.WidthUnits = DimensionUnitType.Absolute;
        offBackground.Dock(Gum.Wireframe.Dock.Left);
        innerContainer.AddChild(offBackground);

        TextureRegion maxBackgroundRegion = atlas.GetRegion(RightSliderRegion);
        // Create the "MAX" side of the slider (right end)
        var maxBackground = new NineSliceRuntime
        {
            Texture = maxBackgroundRegion.Texture,
            TextureAddress = TextureAddress.Custom,
            TextureHeight = maxBackgroundRegion.Height,
            TextureLeft = maxBackgroundRegion.SourceRectangle.Left,
            TextureTop = maxBackgroundRegion.SourceRectangle.Top,
            TextureWidth = maxBackgroundRegion.Width,
            Width = RightSliderPartWidth,
            WidthUnits = DimensionUnitType.Absolute
        };
        maxBackground.Dock(Gum.Wireframe.Dock.Right);
        innerContainer.AddChild(maxBackground);

        TextureRegion middleBackgroundRegion = atlas.GetRegion(MiddleSliderRegion);
        // Create the middle track portion of the slider
        var middleBackground = new NineSliceRuntime();
        middleBackground.Dock(Gum.Wireframe.Dock.FillVertically);
        middleBackground.Texture = middleBackgroundRegion.Texture;
        middleBackground.TextureAddress = TextureAddress.Custom;
        middleBackground.TextureHeight = middleBackgroundRegion.Height;
        middleBackground.TextureLeft = middleBackgroundRegion.SourceRectangle.Left;
        middleBackground.TextureTop = middleBackgroundRegion.SourceRectangle.Top;
        middleBackground.TextureWidth = middleBackgroundRegion.Width;
        middleBackground.Width = MiddleSliderPartWidth;
        middleBackground.WidthUnits = DimensionUnitType.Absolute;
        middleBackground.Dock(Gum.Wireframe.Dock.Left);
        middleBackground.X = LeftSliderPartWidth;
        innerContainer.AddChild(middleBackground);

        // Create the interactive track that responds to clicks
        // The special name "TrackInstance" is required for Slider functionality
        var trackInstance = new ContainerRuntime { Name = "TrackInstance" };
        trackInstance.Dock(Gum.Wireframe.Dock.Fill);
        middleBackground.AddChild(trackInstance);

        // Create the fill rectangle that visually displays the current value
        rangeSelect = new ColoredRectangleRuntime();
        rangeSelect.Dock(Gum.Wireframe.Dock.Left);
        rangeSelect.Width = DefaultSliderValue; // Default to 90% - will be updated by value changes
        rangeSelect.WidthUnits = DimensionUnitType.PercentageOfParent;
        
        trackInstance.AddChild(rangeSelect);

        // Define colors for focused and unfocused states
        Color focusedColor = FocusedColor;
        Color unfocusedColor = UnfocusedColor;

        // Create slider state category - Slider.SliderCategoryName is the required name
        var sliderCategory = new StateSaveCategory
        {
            Name = SliderCategoryName
        };
        topLevelContainer.AddCategory(sliderCategory);

        // Create the enabled (default/unfocused) state
        var enabled = new StateSave
        {
            Name = EnabledStateName,
            Apply = () =>
                {
                    // When enabled but not focused, use gray coloring for all elements
                    background.Color = unfocusedColor;
                    textInstance.Color = unfocusedColor;
                    offBackground.Color = unfocusedColor;
                    middleBackground.Color = unfocusedColor;
                    maxBackground.Color = unfocusedColor;
                    rangeSelect.Color = unfocusedColor;
                }
        };
        sliderCategory.States.Add(enabled);

        // Create the focused state
        var focused = new StateSave
        {
            Name = FocusedStateName,
            Apply = () =>
                {
                    // When focused, use white coloring for all elements
                    background.Color = focusedColor;
                    textInstance.Color = focusedColor;
                    offBackground.Color = focusedColor;
                    middleBackground.Color = focusedColor;
                    maxBackground.Color = focusedColor;
                    rangeSelect.Color = focusedColor;
                }
        };
        sliderCategory.States.Add(focused);

        // Create the highlighted+focused state by cloning the focused state
        StateSave highlightedFocused = focused.Clone();
        highlightedFocused.Name = FrameworkElement.HighlightedFocusedStateName;
        sliderCategory.States.Add(highlightedFocused);

        // Create the highlighted state by cloning the enabled state
        StateSave highlighted = enabled.Clone();
        highlighted.Name = FrameworkElement.HighlightedStateName;
        sliderCategory.States.Add(highlighted);

        // Assign the configured container as this slider's visual
        Visual = topLevelContainer;

        // Enable click-to-point functionality for the slider
        // This allows users to click anywhere on the track to jump to that value
        IsMoveToPointEnabled = true;

        // Add event handlers
        Visual.RollOn += HandleRollOn;
        ValueChanged += HandleValueChanged;
        ValueChangedByUi += HandleValueChangedByUi;
    }

    /// <summary>
    /// Automatically focuses the slider when the user interacts with it
    /// </summary>
    private void HandleValueChangedByUi(object sender, EventArgs e)
    {
        IsFocused = true;
    }

    /// <summary>
    /// Automatically focuses the slider when the mouse hovers over it
    /// </summary>
    private void HandleRollOn(object sender, EventArgs e)
    {
        IsFocused = true;
    }

    /// <summary>
    /// Updates the fill rectangle width to visually represent the current value
    /// </summary>
    private void HandleValueChanged(object sender, EventArgs e)
    {
        // Calculate the ratio of the current value within its range
        double ratio = (Value - Minimum) / (Maximum - Minimum);

        // Update the fill rectangle width as a percentage
        // _fillRectangle uses percentage width units, so we multiply by 100
        rangeSelect.Width = 100 * (float)ratio;
    }
}

