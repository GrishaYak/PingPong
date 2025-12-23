internal class Program
{
    private static void Main(string[] args)
    {
        using var game = new PingPong.PingPong();
        game.Run();
    }
}