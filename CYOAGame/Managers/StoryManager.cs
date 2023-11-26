using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aspose.Words;
using CYOAGame.Classes;
using CYOAGame.Structures.Linked;

namespace CYOAGame.Managers
{
    internal class StoryManager
    {
        public Linkd.Node _currentNode;
        public void RunStory(GameManager gm, Player player)
        {
            Linkd story = new Linkd();
            story.head = new Linkd.Node($"Hello {player.GetName()}, welcome to the wonderfull world of nothingland, please rest while we get to the capital!\n", false);

            Linkd.Node secondNode = new Linkd.Node("You wake up due to some noices, its a monster!\n", true);
            Linkd.Node thirdNode = new Linkd.Node("Story segment!\n", false);
            Linkd.Node fourthNode = new Linkd.Node("Story segment?\n", true);
            Linkd.Node fifthNode = new Linkd.Node("Story segment.\n", false);
            Linkd.Node finalNode = new Linkd.Node("The end\n", true);

            story.head.next = secondNode;
            secondNode.next = thirdNode;
            thirdNode.next = fourthNode;
            fourthNode.next = fifthNode;
            fifthNode.next = finalNode;

            if(_currentNode == null)
            {
                _currentNode = story.head;
            }
            
            story.PrintLinkdList(gm, this, _currentNode);
        }
    }
}
