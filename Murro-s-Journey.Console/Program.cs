using Murro_s_Journey.Console.Core;

namespace Murro_s_Journey.Console;

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();

        while (game.IsRunning)
        {
            game.Update();
            game.Draw();
            System.Threading.Thread.Sleep(100);
        }
    }
}