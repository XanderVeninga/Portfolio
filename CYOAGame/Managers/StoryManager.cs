using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CYOAGame.Classes;
using CYOAGame.Structures.Linked;

namespace CYOAGame.Managers
{
    internal class StoryManager
    {
        public void RunStory(GameManager gm, Player player)
        {
            Linkd story = new Linkd();
            story.head = new Linkd.Node("", false);


            Console.WriteLine($"{player.GetName()}!!!! you wanker wake up! we are being attacked!");
            gm.ChangeGameState(GameState.COMBAT);
        }
    }
}
