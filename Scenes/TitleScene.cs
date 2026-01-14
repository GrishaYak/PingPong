using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLib;
using MonoGameLib.Scenes;
using MonoGameLib.Text;
using MonoGameGum;
using Gum.Forms.Controls;
using System;
using Microsoft.Xna.Framework.Audio;
using PingPong.UI;
using MonoGameLib.Graphics;
using static PingPong.Window;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace PingPong.Scenes
{
    public class TitleScene : Scene
    {
        private MonoLabel TITLE;
        private Font bigFont;
        private SoundEffect clickSoundEffect;
        private Panel titlePanel;
        private MyButton optionsButton;
        private MyButton startButton;
        private Settings settings;
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

            // Change to the game scene to start the game.
            Core.ChangeScene(new GameScene());
        }
        private void HandleOptionsClicked(object sender, EventArgs e)
        {
            // A UI interaction occurred, play the sound effect
            Core.Audio.PlaySoundEffect(clickSoundEffect);
            // Set the title panel to be invisible.

            TitlePanelIsVisible = false;
            // Set the options panel to be visible.

            settings.OptionsPanel.IsVisible = true;
            // Give the back button on the options panel focus.
            settings.Buttons["BackButton"].IsFocused = true;

        }
        private void HandleOptionsButtonBack(object sender, EventArgs e)
        {
            Core.Audio.PlaySoundEffect(clickSoundEffect);

            settings.OptionsPanel.IsVisible = false;

            TitlePanelIsVisible = true;

            optionsButton.IsFocused = true;
        }


        private void InitializeUI()
        {
            GumService.Default.Root.Children.Clear();
            
            CreateTitlePanel();
            settings = new(clickSoundEffect, HandleOptionsButtonBack);
        }

        public override void Initialize()
        {

            Core.ExitOnEscape = true;
            base.Initialize();

            InitializeUI();
            
            startButton.IsFocused = true;
            settings.SetToSaved();
            Core.Audio.PlayNextSong();
        }
        
        public override void LoadContent()
        {
            // Load the font for the title text.
            bigFont = new Font(Content.Load<SpriteFont>("fonts/big"));

            // Load the sound effect to play when ui actions occur.
            clickSoundEffect = Core.Content.Load<SoundEffect>("audio/click");

            Preferences.Atlas = TextureAtlas.FromFile(Core.Content, "textures/atlas.xml");

            Song mainMenu1 = Content.Load<Song>("audio/mainMenu1");
            Song mainMenu2 = Content.Load<Song>("audio/mainMenu2");
            Core.Audio.PlayList = [mainMenu1, mainMenu2];
        }

        public override void Update(GameTime gameTime)
        {
            GumService.Default.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Preferences.Colors.BackgroundColor);

            base.Draw(gameTime);
            DrawLabel();
            GumService.Default.Draw();
        }

        private void DrawLabel()
        {
            if (TitlePanelIsVisible)
            {
                // Begin the sprite batch to prepare for rendering.
                Core.SpriteBatch.PixelBegin();

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
