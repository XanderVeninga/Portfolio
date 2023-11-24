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

        public void PrintLinkdList()
        {
            Node currentNode = head;

            while (currentNode != null)
            {
                Console.WriteLine(currentNode);
                currentNode = currentNode.next;
            }
        }
    }
}
