using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVLtree
{
    public class Node : IComparable
    {
        public int Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public int Height { get; set; }

        public Node(int data, Node lt, Node rt)
        {
            Data = data;
            Left = lt;
            Right = rt;
            Height = 0;
        }
        public Node(int data)
        {
            Data = data;
            Left = null;
            Right = null;
            Height = 0;
        }
        public int CompareTo(Object node)
        {
            return (this.Data.CompareTo(((Node)node).Data));
        }
        public int GetHeight()
        {
            if (this == null)
                return -1;
            else
                return this.Height;
        }
        public void Display()
        {
            Console.WriteLine(Data);
        }
    }

    /// <summary>
    /// Time Complexity: O(log(n))
    /// Space Complexity: O(log(n)) same number of calls on stack. Occupies stack space.
    /// </summary>
    class AVLTree
    {
        public Node Root;
        private int NodeCount;

        public AVLTree()
        {
            Root = null;
            NodeCount = 0;
        }

        public void Insert(int value)
        {            
            Root = Insert(value, Root);
            NodeCount++;
        }

        private Node Insert(int value, Node n)
        {            
            //base case
            if (n == null)
                n = new Node(value, null, null);
            else if (value.CompareTo(n.Data) < 0)           //This instance is less than n.Data
            {
                n.Left = Insert(value, n.Left);
                if (Height(n.Left) - Height(n.Right) == 2)
                {
                    if (value.CompareTo(n.Left.Data) < 0)   //This instance is less than n.Left.Data
                        n = RotateWithLeftChild(n);
                    else
                        n = DoubleWithLeftChild(n);
                }
            }
            else if (value.CompareTo(n.Data) > 0)            //This instance is greater than n.Data
            {
                n.Right = Insert(value, n.Right);
                if (Height(n.Right) - Height(n.Left) == 2)
                {
                    if (value.CompareTo(n.Right.Data) > 0)  //This instance is greater than n.Right.Data
                        n = RotateWithRightChild(n);
                    else
                        n = DoubleWithRightChild(n);
                }
            }
            //else
                // do nothing, duplicate value

            n.Height = Math.Max(Height(n.Left), Height(n.Right)) + 1;
            return n;
        }

        private int Height(Node n)
        {
            return n == null ? -1 : n.Height;
        }

        private Node RotateWithLeftChild(Node n2)
        {
            Node n1 = n2.Left;
            n2.Left = n1.Right;
            n1.Right = n2;
            n2.Height = Math.Max(Height(n2.Left), Height(n2.Right)) + 1;
            n1.Height = Math.Max(Height(n1.Left), Height(n2)) + 1;
            return n1;
        }
        private Node RotateWithRightChild(Node n1)
        {
            Node n2 = n1.Right;
            n1.Right = n2.Left;
            n2.Left = n1;
            n1.Height = Math.Max(Height(n1.Left), Height(n1.Right)) + 1;
            n2.Height = Math.Max(Height(n2.Right), Height(n1)) + 1;
            return n2;
        }
        private Node DoubleWithLeftChild(Node n3)
        {
            n3.Left = RotateWithRightChild(n3.Left);
            return RotateWithLeftChild(n3);
        }
        private Node DoubleWithRightChild(Node n1)
        {
            n1.Right = RotateWithLeftChild(n1.Right);
            return RotateWithRightChild(n1);
        }

        public void InOrder(Node root)
        {
            if (root != null)
            {
                InOrder(root.Left);
                root.Display();
                InOrder(root.Right);
            }
        }

        public void PreOrder(Node root)
        {
            if (root != null)
            {
                root.Display();
                PreOrder(root.Left);
                PreOrder(root.Right);
            }
        }

        public void PostOrder(Node root)
        {
            if (root != null)
            {
                PostOrder(root.Left);
                PostOrder(root.Right);
                root.Display();
            }
        }

        public int? FindMin()
        {
            Node current = Root;
            if (current == null)
                return null;
            while (current.Left != null)
                current = current.Left;
            return current.Data;
        }

        public int? FindMax()
        {
            Node current = Root;
            if (current != null)
                return null;
            while (current.Right != null)
                current = current.Right;
            return current.Data;
        }

        public Node Find(int value)
        {
            Node current = Root;
            while(current!=null && current.Data!=value)
            {
                if (current.Left != null && value < current.Data)
                    current = current.Left;
                else
                    current = current.Right;
            }
            return current;
        }

        public int GetEdgeCount()
        {
            return NodeCount - 1;
        }

        public int GetNodeCount()
        {
            return NodeCount;
        }

        //for delete function:
        //Add isDeleted property to node class and do a soft delete. More efficient.
    }

    class Program
    {
        static void Main(string[] args)
        {
            AVLTree avl = new AVLTree();
            avl.Insert(6);
            avl.Insert(5);
            avl.Insert(3);
            avl.Insert(9);
            avl.Insert(8);            
            Console.WriteLine("Inorder traversal: ");
            avl.InOrder(avl.Root);
            Console.WriteLine("Preorder traversal: ");
            avl.PreOrder(avl.Root);
            Console.WriteLine("Postorder traversal: ");
            avl.PostOrder(avl.Root);

            AVLTree avl1 = new AVLTree();
            avl1.Insert(6);
            avl1.Insert(5);
            avl1.Insert(9);
            avl1.Insert(3);
            avl1.Insert(8);
            avl1.Insert(7);
            Console.WriteLine("Inorder traversal: ");
            avl1.InOrder(avl1.Root);
            Console.WriteLine("Preorder traversal: ");
            avl1.PreOrder(avl1.Root);
            Console.WriteLine("Postorder traversal: ");
            avl1.PostOrder(avl1.Root);
        }
    }
}
