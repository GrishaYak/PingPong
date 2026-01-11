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
using System.Collections.Generic;
using System.Linq;

namespace PingPong.Scenes
{
    public class TitleScene : Scene
    {
        private MonoLabel TITLE;
        private Font bigFont;
        private SoundEffect clickSoundEffect;
        private Panel titlePanel;
        private Panel optionsPanel;
        private MyButton optionsButton;
        private MyButton optionsBackButton;
        private MyButton startButton;
        private void CreateTitlePanel()
        {
            TextManager.SetParent("menu");

            TITLE = new MonoLabel(TextManager.Get("title"));
            TITLE.Pos = new Vector2(ScreenWidth, ScreenHeight) * 0.5f + new Vector2(0, -50);
            TITLE.Origin = bigFont.MeasureString(TITLE.Text) * 0.5f;
            bigFont.AddLabel(TITLE);

            titlePanel = new Panel();
            titlePanel.Dock(Gum.Wireframe.Dock.Fill);
            titlePanel.AddToRoot();

            startButton = new MyButton(Preferences.Atlas, text: TextManager.Get("play"));
            startButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
            startButton.Visual.X = BufferWidth * 0.125f;
            startButton.Visual.Y = -BufferHeight * 0.15f;
            startButton.Click += HandleStartClicked;
            titlePanel.AddChild(startButton);

            optionsButton = new MyButton(Preferences.Atlas, text: TextManager.Get("options"));
            optionsButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
            optionsButton.Visual.X = -BufferWidth * 0.125f;
            optionsButton.Visual.Y = -BufferHeight * 0.15f;
            optionsButton.Click += HandleOptionsClicked;
            titlePanel.AddChild(optionsButton);
            
        }
        private void HandleStartClicked(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            Core.Audio.PlaySoundEffect(clickSoundEffect);

            System.Console.WriteLine(startButton.IsEnabled);
            // Change to the game scene to start the game.
            Core.ChangeScene(new GameScene());
        }
        private void HandleOptionsClicked(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            Core.Audio.PlaySoundEffect(clickSoundEffect);
            System.Console.WriteLine("Options!");
            // Set the title panel to be invisible.

            TitlePanelIsVisible = false;
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
                Color = Preferences.OptionsPanel.OptionsText.Color
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
        private void HandleSfxSliderChanged(object sender, EventArgs args) { var slider = (Slider)sender; Core.Audio.SoundEffectVolume = (float)slider.Value; } 
        private void HandleSfxSliderChangeCompleted(object sender, EventArgs e) { Core.Audio.PlaySoundEffect(clickSoundEffect); }
        private void HandleMusicSliderValueChanged(object sender, EventArgs args) { var slider = (Slider)sender; Core.Audio.SongVolume = (float)slider.Value; }
        private void HandleMusicSliderValueChangeCompleted(object sender, EventArgs args) { Core.Audio.PlaySoundEffect(clickSoundEffect); }
        private void HandleOptionsButtonBack(object sender, EventArgs e)
        {
            System.Console.WriteLine(startButton.IsEnabled);
            Core.Audio.PlaySoundEffect(clickSoundEffect);

            optionsPanel.IsVisible = false;

            TitlePanelIsVisible = true;

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
            startButton.IsFocused=true;
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
            DrawLabel();
            GumService.Default.Draw();
        }

        private void DrawLabel()
        {
            if (TitlePanelIsVisible)
            {
                // Begin the sprite batch to prepare for rendering.
                Core.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);

                bigFont.Draw(Core.SpriteBatch);

                Core.SpriteBatch.End();
            }
            
        }

        private bool TitlePanelIsVisible
        {
            get => titlePanel.IsVisible;
            set
            {
                startButton.IsEnabled = value;
                optionsButton.IsEnabled = value;
                titlePanel.IsVisible = value;
            }
        }

    }
}
