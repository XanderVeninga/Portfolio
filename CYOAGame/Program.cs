using System.Numerics;
using CYOAGame.Managers;
using CYOAGame.SuperClasses;

namespace CYOAGame
{

    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gm = new GameManager();

            gm.RunGame();
        }
    }
}