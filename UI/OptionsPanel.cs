using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLib;
using MonoGameLib.Scenes;
using MonoGameLib.Text;
using MonoGameGum.GueDeriving;
using MonoGameGum;
using Gum.Forms.Controls;
using System;
using Microsoft.Xna.Framework.Audio;
using PingPong.UI;
using MonoGameLib.Graphics;
using static PingPong.Window;

namespace PingPong.UI;

public class OptionsPanel
{
    private SoundEffect clickSoundEffect;
    public Panel Get(SoundEffect clickSoundEffect, EventHandler HandleOptionsButtonBack)
    {
        TextManager.SetParent("options");
        this.clickSoundEffect = clickSoundEffect;
        var optionsPanel = new Panel();
        optionsPanel.Dock(Gum.Wireframe.Dock.Fill);
        optionsPanel.IsVisible = false;
        optionsPanel.AddToRoot();

        var optionsText = new TextRuntime
        {
            X = Preferences.OptionsPanel.OptionsText.X,
            Y = Preferences.OptionsPanel.OptionsText.Y,
            Text = TextManager.Get("options"),
            UseCustomFont = true,
            FontScale = Preferences.OptionsPanel.OptionsText.FontScale,
            CustomFontFile = Preferences.Font.FontFile,
        };

        optionsPanel.AddChild(optionsText);

        var musicSlider = new OptionsSlider(Preferences.Atlas, text: TextManager.Get("music"));
        musicSlider.Name = "MusicSlider";
        musicSlider.Anchor(Gum.Wireframe.Anchor.TopLeft);
        musicSlider.Visual.X = Preferences.SliderWithText.TotalX;
        musicSlider.Visual.Y = Preferences.OptionsPanel.FirstSliderY;
        musicSlider.Minimum = 0;
        musicSlider.Maximum = 1;
        musicSlider.Value = Core.Audio.SongVolume;
        musicSlider.SmallChange = Preferences.SliderWithText.SmallChange;
        musicSlider.LargeChange = Preferences.SliderWithText.LargeChange;
        musicSlider.ValueChanged += HandleMusicSliderValueChanged;
        musicSlider.ValueChangeCompleted += HandleMusicSliderValueChangeCompleted;
        optionsPanel.AddChild(musicSlider);

        var sfxSlider = new OptionsSlider(Preferences.Atlas, text: TextManager.Get("sfx"));
        sfxSlider.Name = "SfxSlider";
        sfxSlider.Anchor(Gum.Wireframe.Anchor.TopLeft);
        sfxSlider.Visual.X = Preferences.SliderWithText.TotalX;
        sfxSlider.Visual.Y = musicSlider.Visual.Y + Preferences.SliderWithText.StandardIndent;
        sfxSlider.Minimum = 0;
        sfxSlider.Maximum = 1;
        sfxSlider.Value = Core.Audio.SoundEffectVolume;
        sfxSlider.SmallChange = Preferences.SliderWithText.SmallChange;
        sfxSlider.LargeChange = Preferences.SliderWithText.LargeChange;
        sfxSlider.ValueChanged += HandleSfxSliderChanged;
        sfxSlider.ValueChangeCompleted += HandleSfxSliderChangeCompleted;
        optionsPanel.AddChild(sfxSlider);

        var optionsBackButton = new MyButton(Preferences.Atlas, text: TextManager.Get("back"));
        optionsBackButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        optionsBackButton.X = Preferences.OptionsPanel.Button.X;
        optionsBackButton.Y = Preferences.OptionsPanel.Button.Y;
        optionsBackButton.Click += HandleOptionsButtonBack;
        optionsPanel.AddChild(optionsBackButton);
        return optionsPanel;
    }

        private void HandleSfxSliderChanged(object sender, EventArgs args)
    {
        // Get a reference to the sender as a Slider.
        var slider = (Slider)sender;

        Core.Audio.SoundEffectVolume = (float)slider.Value;
    }
    private void HandleSfxSliderChangeCompleted(object sender, EventArgs e)
    {
        Core.Audio.PlaySoundEffect(clickSoundEffect);
    }
    private void HandleMusicSliderValueChanged(object sender, EventArgs args)
    {
        // Intentionally not playing the UI sound effect here so that it is not
        // constantly triggered as the user adjusts the slider's thumb on the
        // track.

        // Get a reference to the sender as a Slider.
        var slider = (Slider)sender;

        // Set the global song volume to the value of the slider.
        Core.Audio.SongVolume = (float)slider.Value;
    }
    private void HandleMusicSliderValueChangeCompleted(object sender, EventArgs args)
        {
            // A UI interaction occurred, play the sound effect
            Core.Audio.PlaySoundEffect(clickSoundEffect);
        }
}
