using MonoGameLib.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PingPong.UI;
using MonoGameGum;
using Microsoft.Xna.Framework;
using MonoGameLib;
using MonoGameLib.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using static PingPong.Window;
using MonoGameLib.Graphics;


namespace PingPong.Scenes;
public class GameScene : Scene
{
    SpriteFont scoreFont;
    SoundEffect clickSoundEffect;
    TwoPlayerScore score;
    Ball ball;
    private void InitializeUI()
    {
        GumService.Default.Root.Children.Clear();
        score = new(scoreFont);
    }
    public override void Initialize()
    {
        ball = new();
        ball.OutOfScreen += OnBallOutOfScreen;
        base.Initialize();
        InitializeUI();
        ball.Start();
    }

    public override void LoadContent()
    {
        scoreFont = Content.Load<SpriteFont>("fonts/big");
        clickSoundEffect = Core.Content.Load<SoundEffect>("audio/click");
        ball.LoadTexture(Preferences.Atlas.GetRegion("ball"));
    }
    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Preferences.Colors.BackgroundColor);
        Core.SpriteBatch.PixelBegin();
        score.Draw(Core.SpriteBatch);
        ball.Draw(Core.SpriteBatch);
        Core.SpriteBatch.End();
        GumService.Default.Draw();
    }
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        ball.Update(gameTime);
    }
    private void OnBallOutOfScreen()
    {
        ball.Reset();
        ball.Start();
    }
}

