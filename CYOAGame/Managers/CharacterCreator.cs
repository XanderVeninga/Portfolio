using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using CYOAGame.Classes;

namespace CYOAGame.Managers
{

    internal class CharacterCreator
    {
        Player player;
        public Player NewCharacter(GameManager gm)
        {
            Console.WriteLine("Hello player what is your name?\n");
            string playerName = Console.ReadLine();
            Console.Clear();
            player = new Player(playerName, 0, 0, 0, 1, 0);
            Console.WriteLine($"Ok {player.GetName()}, what difficutly do you want to play on?\n\n[1]: Easy\n[2]: Medium\n[3]: Hard\n");
            int difficutly;
            if (Int32.TryParse(Console.ReadLine(), out difficutly))
            {
                player.SetDamage(MathF.Round(40 / difficutly));
                player.ChangeMaxHealth(MathF.Round(250 / difficutly));
                player.SetHealth(MathF.Round(250 / difficutly));
            }
            gm.difficulty = difficutly;
            gm.ChangeGameState(GameState.STORY);
            Console.Clear();
            return player;
        }
    }
}
