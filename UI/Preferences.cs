using static PingPong.Window;

namespace PingPong.UI;

public static class Preferences
{
    public static class Button
    {
        public static float Width {get;}= BufferWidth * 0.15f;
        public static float Height {get;} = BufferHeight * 0.095f;
        public static float TextIndent {get;} = BufferMultiplyer * 4;
    }
}
