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

namespace PingPong.Scenes
{
    public class TitleScene : Scene
    {
        private Vector2 _screen_reso;

        private MonoLabel TITLE;

        private Font defaultFont;
        private Font bigFont;
        private SoundEffect _clickSoundEffect;
        private Panel _titleScreenButtonsPanel;
        private Panel _optionsPanel;
        private Button _optionsButton;
        private Button _optionsBackButton;
        
        private void CreateTitlePanel()
        {
            _titleScreenButtonsPanel = new Panel();
            _titleScreenButtonsPanel.Dock(Gum.Wireframe.Dock.Fill);
            _titleScreenButtonsPanel.AddToRoot();

            var startButton = new Button();
            startButton.Anchor(Gum.Wireframe.Anchor.BottomLeft);
            startButton.Visual.X = 20;
            startButton.Visual.Y = -20;
            startButton.Visual.Width = 50;
            startButton.Text = "Start";
            startButton.Click += HandleStartClicked;
            _titleScreenButtonsPanel.AddChild(startButton);

            _optionsButton = new Button();
            _optionsButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
            _optionsButton.Visual.X = -20;
            _optionsButton.Visual.Y = -20;
            _optionsButton.Visual.Width = 50;
            _optionsButton.Text = "Options";
            _optionsButton.Click += HandleOptionsClicked;
            _titleScreenButtonsPanel.AddChild(_optionsButton);

            startButton.IsFocused = true;
        }
        private void HandleStartClicked(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            Core.Audio.PlaySoundEffect(_clickSoundEffect);

            // Change to the game scene to start the game.
            Core.ChangeScene(new GameScene());
        }
        private void HandleOptionsClicked(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            Core.Audio.PlaySoundEffect(_clickSoundEffect);

            // Set the title panel to be invisible.
            _titleScreenButtonsPanel.IsVisible = false;

            // Set the options panel to be visible.
            _optionsPanel.IsVisible = true;

            // Give the back button on the options panel focus.
            _optionsBackButton.IsFocused = true;
        }
        private void CreateOptionsPanel()
        {
            _optionsPanel = new Panel();
            _optionsPanel.Dock(Gum.Wireframe.Dock.Fill);
            _optionsPanel.IsVisible = false;
            _optionsPanel.AddToRoot();

            var optionsText = new TextRuntime();
            optionsText.X = 10;
            optionsText.Y = 10;
            optionsText.Text = "OPTIONS";
            _optionsPanel.AddChild(optionsText);

            var musicSlider = new Slider();
            musicSlider.Anchor(Gum.Wireframe.Anchor.Top);
            musicSlider.Visual.Y = 40;
            musicSlider.Minimum = 0;
            musicSlider.Maximum = 1;
            musicSlider.Value = Core.Audio.SongVolume;
            musicSlider.SmallChange = .1;
            musicSlider.LargeChange = .2;
            musicSlider.ValueChanged += HandleMusicSliderValueChanged;
            musicSlider.ValueChangeCompleted += HandleMusicSliderValueChangeCompleted;
            _optionsPanel.AddChild(musicSlider);

            var sfxSlider = new Slider();
            sfxSlider.Anchor(Gum.Wireframe.Anchor.Top);
            sfxSlider.Visual.Y = 70;
            sfxSlider.Minimum = 0;
            sfxSlider.Maximum = 1;
            sfxSlider.Value = Core.Audio.SoundEffectVolume;
            sfxSlider.SmallChange = .1;
            sfxSlider.LargeChange = .2;
            sfxSlider.ValueChanged += HandleSfxSliderChanged;
            sfxSlider.ValueChangeCompleted += HandleSfxSliderChangeCompleted;
            _optionsPanel.AddChild(sfxSlider);

            _optionsBackButton = new Button();
            _optionsBackButton.Text = "BACK";
            _optionsBackButton.Anchor(Gum.Wireframe.Anchor.BottomRight);
            _optionsBackButton.X = -20f;
            _optionsBackButton.Y = -20f;
            _optionsBackButton.Click += HandleOptionsButtonBack;
            _optionsPanel.AddChild(_optionsBackButton);
        }
        private void HandleSfxSliderChanged(object sender, EventArgs args)
        {
            // Get a reference to the sender as a Slider.
            var slider = (Slider)sender;

            Core.Audio.SoundEffectVolume = (float)slider.Value;
        }
        private void HandleSfxSliderChangeCompleted(object sender, EventArgs e)
        {
            Core.Audio.PlaySoundEffect(_clickSoundEffect);
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
            Core.Audio.PlaySoundEffect(_clickSoundEffect);
        }
        private void HandleOptionsButtonBack(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            Core.Audio.PlaySoundEffect(_clickSoundEffect);

            _titleScreenButtonsPanel.IsVisible = true;

            _optionsPanel.IsVisible = false;

            _optionsButton.IsFocused = true;
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
            _screen_reso = new Vector2(900, 500);

            // LoadContent is called during base.Initialize().
            base.Initialize();

            TITLE = new MonoLabel("AeroPong");
            TITLE.Pos = _screen_reso * 0.5f + new Vector2(0, -50);
            TITLE.Origin = bigFont.MeasureString(TITLE.Text) * 0.5f;
            bigFont.AddLabel(TITLE);

            InitializeUI();

        }

        public override void LoadContent()
        {
            // Load the font for the standard text.
            defaultFont = new Font(Core.Content.Load<SpriteFont>("fonts/04B_30"));

            // Load the font for the title text.
            bigFont = new Font(Content.Load<SpriteFont>("fonts/04B_30_5x"));

            // Load the sound effect to play when ui actions occur.
            _clickSoundEffect = Core.Content.Load<SoundEffect>("audio/click");
        }

        public override void Update(GameTime gameTime)
        {
            GumService.Default.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.Gray * 0.25f);

            
            base.Draw(gameTime);
            if (_titleScreenButtonsPanel.IsVisible)
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
