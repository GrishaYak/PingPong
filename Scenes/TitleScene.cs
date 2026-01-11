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

namespace PingPong.Scenes
{
    public class TitleScene : Scene
    {
        private MonoLabel TITLE;
        private Font bigFont;
        private SoundEffect clickSoundEffect;
        private Panel titleScreenButtonsPanel;
        private Panel optionsPanel;
        private MyButton optionsButton;
        private MyButton optionsBackButton;
        private void CreateTitlePanel()
        {
            TextManager.SetParent("menu");

            TITLE = new MonoLabel(TextManager.Get("title"));
            TITLE.Pos = new Vector2(ScreenWidth, ScreenHeight) * 0.5f + new Vector2(0, -50);
            TITLE.Origin = bigFont.MeasureString(TITLE.Text) * 0.5f;
            bigFont.AddLabel(TITLE);

            titleScreenButtonsPanel = new Panel();
            titleScreenButtonsPanel.Dock(Gum.Wireframe.Dock.Fill);
            titleScreenButtonsPanel.AddToRoot();

            var startButton = new MyButton(Preferences.Atlas, text: TextManager.Get("play"));
            startButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
            startButton.Visual.X = BufferWidth * 0.125f;
            startButton.Visual.Y = -BufferHeight * 0.15f;
            startButton.Click += HandleStartClicked;

            titleScreenButtonsPanel.AddChild(startButton);

            optionsButton = new MyButton(Preferences.Atlas, text: TextManager.Get("options"));
            optionsButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
            optionsButton.Visual.X = -BufferWidth * 0.125f;
            optionsButton.Visual.Y = -BufferHeight * 0.15f;
            optionsButton.Click += HandleOptionsClicked;
            titleScreenButtonsPanel.AddChild(optionsButton);

            startButton.IsFocused = true;
            
        }
        private void HandleStartClicked(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            Core.Audio.PlaySoundEffect(clickSoundEffect);

            System.Console.WriteLine("Starting!");
            // Change to the game scene to start the game.
            Core.ChangeScene(new GameScene());
        }
        private void HandleOptionsClicked(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            Core.Audio.PlaySoundEffect(clickSoundEffect);

            // Set the title panel to be invisible.
            titleScreenButtonsPanel.IsVisible = false;
            titleScreenButtonsPanel.IsEnabled = false;

            // Set the options panel to be visible.
            optionsPanel.IsVisible = true;

            // Give the back button on the options panel focus.
            optionsBackButton.IsFocused = true;
        }
        private void CreateOptionsPanel()
        {
            TextManager.SetParent("options");
            
            optionsPanel = new Panel();
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

            optionsBackButton = new MyButton(Preferences.Atlas, text: TextManager.Get("back"));
            optionsBackButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
            optionsBackButton.X = Preferences.OptionsPanel.Button.X;
            optionsBackButton.Y = Preferences.OptionsPanel.Button.Y;
            optionsBackButton.Click += HandleOptionsButtonBack;
            optionsPanel.AddChild(optionsBackButton);
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
        private void HandleOptionsButtonBack(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            Core.Audio.PlaySoundEffect(clickSoundEffect);

            titleScreenButtonsPanel.IsVisible = true;

            optionsPanel.IsVisible = false;

            optionsButton.IsFocused = true;
        }
        private void InitializeUI()
        {
            GumService.Default.Root.Children.Clear();

            CreateTitlePanel();
            CreateOptionsPanel();
        }

        public override void Initialize()
        {

            Core.ExitOnEscape = true;
            base.Initialize();

            InitializeUI();

        }

        public override void LoadContent()
        {
            // Load the font for the title text.
            bigFont = new Font(Content.Load<SpriteFont>("fonts/big"));

            // Load the sound effect to play when ui actions occur.
            clickSoundEffect = Core.Content.Load<SoundEffect>("audio/click");

            Preferences.Atlas = TextureAtlas.FromFile(Core.Content, "textures/atlas.xml");
        }

        public override void Update(GameTime gameTime)
        {
            GumService.Default.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Gray * 0.25f);


            base.Draw(gameTime);
            if (titleScreenButtonsPanel.IsVisible)
            {
                // Begin the sprite batch to prepare for rendering.
                Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

                bigFont.Draw(Core.SpriteBatch);

                Core.SpriteBatch.End();
            }

            GumService.Default.Draw();
        }

    }
}
