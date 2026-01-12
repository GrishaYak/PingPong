using MonoGameLib;
using MonoGameLib.Text;
using MonoGameGum.GueDeriving;
using MonoGameGum;
using Gum.Forms.Controls;
using System;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using static PingPong.UI.Preferences.Settings;

namespace PingPong.UI;

public class Settings
{
    public Panel OptionsPanel;

    public List<OptionsSlider> Sliders = [];
    public Dictionary<string, MyButton> Buttons = [];
    private SoundEffect clickSoundEffect;
    public Settings(SoundEffect click, EventHandler HandleOptionsButtonBack)
    {
        clickSoundEffect = click;
        TextManager.SetParent("options");

        OptionsPanel = new Panel();
        OptionsPanel.Dock(Gum.Wireframe.Dock.Fill);
        OptionsPanel.IsVisible = false;
        OptionsPanel.AddToRoot();

        var optionsText = new TextRuntime
        {
            X = OptionsText.X,
            Y = OptionsText.Y,
            Text = TextManager.Get("options"),
            UseCustomFont = true,
            FontScale = OptionsText.FontScale,
            CustomFontFile = Preferences.Font.FontFile,
            Color = OptionsText.Color
        };

        OptionsPanel.AddChild(optionsText);

        var musicSlider = new OptionsSlider(Preferences.Atlas, text: TextManager.Get("music"));
        musicSlider.Name = "MusicSlider";
        musicSlider.Anchor(Gum.Wireframe.Anchor.TopLeft);
        musicSlider.Visual.X = Preferences.SliderWithText.TotalX;
        musicSlider.Visual.Y = FirstSliderY;
        musicSlider.Minimum = 0;
        musicSlider.Maximum = 1;
        musicSlider.Value = Core.Audio.SongVolume;
        musicSlider.SmallChange = Preferences.SliderWithText.SmallChange;
        musicSlider.LargeChange = Preferences.SliderWithText.LargeChange;
        musicSlider.ValueChanged += HandleMusicSliderValueChanged;
        musicSlider.ValueChangeCompleted += HandleMusicSliderValueChangeCompleted;
        OptionsPanel.AddChild(musicSlider);
        Sliders.Add(musicSlider);

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
        OptionsPanel.AddChild(sfxSlider);
        Sliders.Add(sfxSlider);

        var optionsSaveButton = new MyButton(Preferences.Atlas, text: TextManager.Get("save"));
        optionsSaveButton.Name = "SaveButton";
        optionsSaveButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        optionsSaveButton.X = SaveButton.X;
        optionsSaveButton.Y = SaveButton.Y;
        optionsSaveButton.Click += HandleOptionsSave;
        OptionsPanel.AddChild(optionsSaveButton);
        AddButtonToDict(optionsSaveButton);

        var optionsDiscardButton = new MyButton(Preferences.Atlas, text: TextManager.Get("discard"));
        optionsDiscardButton.Name = "DiscardButton";
        optionsDiscardButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        optionsDiscardButton.X = DiscardButton.X;
        optionsDiscardButton.Y = DiscardButton.Y;
        optionsDiscardButton.Click += HandleOptionsDiscard;
        OptionsPanel.AddChild(optionsDiscardButton);
        AddButtonToDict(optionsDiscardButton);

        var optionsBackButton = new MyButton(Preferences.Atlas, text: TextManager.Get("back"));
        optionsBackButton.Name = "BackButton";
        optionsBackButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
        optionsBackButton.X = BackButton.X;
        optionsBackButton.Y = BackButton.Y;
        optionsBackButton.Click += HandleOptionsButtonBack;
        OptionsPanel.AddChild(optionsBackButton);
        AddButtonToDict(optionsBackButton);

    }
    private void HandleSfxSliderChanged(object sender, EventArgs args) { var slider = (Slider)sender; Core.Audio.SoundEffectVolume = (float)slider.Value; }
    private void HandleSfxSliderChangeCompleted(object sender, EventArgs e) { Core.Audio.PlaySoundEffect(clickSoundEffect); }
    private void HandleMusicSliderValueChanged(object sender, EventArgs args) { var slider = (Slider)sender; Core.Audio.SongVolume = (float)slider.Value; }
    private void HandleMusicSliderValueChangeCompleted(object sender, EventArgs args) { Core.Audio.PlaySoundEffect(clickSoundEffect); }
    private void HandleOptionsSave(object sender, EventArgs e)
    {
        Core.Audio.PlaySoundEffect(clickSoundEffect);
        string prefs = JsonSerializer.Serialize(GetSettings());
        File.WriteAllText(PathToSettings, prefs);
    }
    private void HandleOptionsDiscard(object sender, EventArgs e)
    {
        Core.Audio.MuteAudio();
        SetSettings(ReadSettings());
        Core.Audio.UnmuteAudio();
        Core.Audio.PlaySoundEffect(clickSoundEffect);
    }
    public Dictionary<string, double> GetSettings()
    {
        Dictionary<string, double> result = [];
        foreach (var slider in Sliders)
        {
            result[slider.Name] = slider.Value;
        }
        return result;
    }

    public Dictionary<string, double> ReadSettings()
    {
        return JsonSerializer.Deserialize<Dictionary<string, double>>(File.ReadAllText(PathToSettings));
    }

    public void SetSettings(Dictionary<string, double> settings)
    {
        foreach (var slider in Sliders)
        {
            slider.Value = settings[slider.Name];

        }
    }
    private void AddButtonToDict(MyButton button)
    {
        Buttons[button.Name] = button;
    }
}
