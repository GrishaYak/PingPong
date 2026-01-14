using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static PingPong.Window;

namespace PingPong.UI;

public abstract class Score
{
    protected SpriteFont _font;
    protected void RecalculateOrigin() { Origin = _font.MeasureString(OutString) * new Vector2(0.5f, 0f); }
    public abstract void Reset();
    public abstract string OutString {get;}
    public Vector2 Position { get; } = new Vector2(ScreenWidth * 0.5f, 0);
    public Vector2 Origin { get; set; }
    public void Draw(SpriteBatch spriteBatch, float scale = 1f, float depth = 0f)
    {
        spriteBatch.DrawString(_font, OutString, Position, Color.White, 0.0f, Origin, scale, SpriteEffects.None, depth);
    }
}
