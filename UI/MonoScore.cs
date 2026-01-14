using Microsoft.Xna.Framework.Graphics;

namespace PingPong.UI;

public class MonoScore : Score
{
    public MonoScore(SpriteFont font)
    {
        _font = font;
        Value = 0;
    }
    private int _value;
    public int Value
    {
        get => _value; set
        {
            _value = value;
            RecalculateOrigin();
        }
    }
    public void AddOne() { Value += 1; }
    public override void Reset() { Value = 0; }
    public override string OutString {get => $"{Value}";}
}
