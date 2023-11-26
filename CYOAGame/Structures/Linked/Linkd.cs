using Aspose.Words;
using CYOAGame.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYOAGame.Structures.Linked
{
    class Linkd
    {
        public Node head;
        public Node currentNode;
        public class Node
        {
            public string data;
            public bool isCombat;
            public Node next;

            public Node(string d, bool c)
            {
                data = d;
                isCombat = c;
            }
        }
        public class ChoiceNode : Node
        {
            public Node otherNext;

            public ChoiceNode(string d, bool c) : base(d,c)
            {

            }
        }

        public void PrintLinkdList(GameManager gm, StoryManager story, Node newNode)
        {
            currentNode = newNode;
            while (currentNode != null)
            {
                Console.WriteLine(currentNode.data);
                Console.WriteLine("Press Enter to continue.\n");
                Console.ReadLine();
                if (currentNode.isCombat)
                {
                    gm.ChangeGameState(GameState.COMBAT);
                    story._currentNode = currentNode.next;
                    break;
                }
                else
                {
                    currentNode = currentNode.next;
                }
                Console.Clear();
            }
        }
    }
}
