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

namespace PingPong.Scenes;
public class GameScene : Scene
{
    SpriteFont scoreFont;
    SoundEffect clickSoundEffect;
    Score score;
    private class Score
    {
        private readonly SpriteFont _font;
        public Score(SpriteFont font)
        {
            _font = font;
            Value = 0;
        }
        private int _value;
        public int Value {get => _value; set
            {
                _value = value;
                Origin = _font.MeasureString(Value.ToString()) * new Vector2(0.5f, 0f);
            }
        }
        public void AddOne() {Value += 1;}
        public void Reset() {Value = 0;}
        public Vector2 Position {get;} = new Vector2(ScreenWidth*0.5f, 0);
        public Vector2 Origin {get; set;} 
    }
    private void InitializeUI()
    {
        GumService.Default.Root.Children.Clear();
        score = new(scoreFont);
    }
    public override void Initialize()
    {
        base.Initialize();
        InitializeUI();
    }

    public override void LoadContent()
    {
        scoreFont = Content.Load<SpriteFont>("fonts/big");
        clickSoundEffect = Core.Content.Load<SoundEffect>("audio/click");
    }
    public override void Draw(GameTime gameTime)
    {
        Core.GraphicsDevice.Clear(Color.Gray * 0.25f);
        Core.SpriteBatch.Begin();
        Core.SpriteBatch.DrawString(scoreFont, $"{score.Value}", score.Position, Color.White, 0.0f, score.Origin, 1f, SpriteEffects.None, 0.0f );
        Core.SpriteBatch.End();
        GumService.Default.Draw();
    }
}

