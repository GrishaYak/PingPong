using Microsoft.Xna.Framework.Graphics;

namespace PingPong.UI;

public class TwoPlayerScore : Score
{
    public TwoPlayerScore(SpriteFont font)
    {
        _font = font;
        Reset();
    }
    private int _leftValue;
    public int LeftValue
    {
        get => _leftValue; set
        {
            _leftValue = value;
            RecalculateOrigin();
        }
    }
    private int _rightValue;
    public int RightValue
    {
        get => _rightValue; set
        {
            _rightValue = value;
            RecalculateOrigin();
        }
    }

    public override string OutString { get => $"{LeftValue} : {RightValue}"; }

    public void RightAddOne() { RightValue += 1; }
    public void LeftAddOne() { LeftValue += 1; }
    public override void Reset() { _leftValue = 0; RightValue = 0;}
}
