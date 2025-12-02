using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLib;
using MonoGameLib.Scenes;
using MonoGameLib.Text;

namespace PingPong.Scenes
{
    public class TitleScene : Scene
    {
        private Vector2 _screen_reso;

        private Label TITLE;

        private Font defaultFont;
        private Font font5x;

        public override void Initialize()
        {
            Core.ExitOnEscape = true;
            _screen_reso = new Vector2(900, 500);

            // LoadContent is called during base.Initialize().
            base.Initialize();

            TITLE = new Label("AeroPong");
            TITLE.Pos = _screen_reso * 0.5f + new Vector2(0, -50);
            TITLE.Origin = font5x.MeasureString(TITLE.Text) * 0.5f;
        }

        public override void LoadContent()
        {
            // Load the font for the standard text.
            defaultFont.SpriteFont = Core.Content.Load<SpriteFont>("fonts/04B_30");

            // Load the font for the title text.
            font5x.SpriteFont = Content.Load<SpriteFont>("fonts/04B_30_5x");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Core.GraphicsDevice.Clear(Color.DarkSlateGray);

            Core.SpriteBatch.Begin();
            font5x.Draw(Core.SpriteBatch);
            Core.SpriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
