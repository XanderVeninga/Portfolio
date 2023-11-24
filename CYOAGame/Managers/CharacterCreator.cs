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
            Console.WriteLine("Hello player what is your name");
            string playerName = Console.ReadLine();
            player = new Player(playerName, 0, 0, 0, 0);
            Console.WriteLine($"ok {player.GetName()}, what difficutly do you want to play on\n[1]: Easy\n[2]: Medium\n[3]: Hard");
            int difficutly;
            if (Int32.TryParse(Console.ReadLine(), out difficutly))
            {
                player.SetDamage(MathF.Round(10 / difficutly));
                player.SetHealth(MathF.Round(100 / difficutly));
            }
            gm.difficulty = difficutly;
            gm.ChangeGameState(GameState.STORY);
            return player;
        }
    }
}
