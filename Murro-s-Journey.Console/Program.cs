using Murro_s_Journey.Console.States;

namespace Murro_s_Journey.Console;

class Program
{
    static void Main(string[] args)
    {
        GameContext game = new GameContext();
        
        while (true)
        {
            game.Update();
            System.Threading.Thread.Sleep(100);
        }
    }
}