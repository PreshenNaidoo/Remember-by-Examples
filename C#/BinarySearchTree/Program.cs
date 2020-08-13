using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree
{
    public class Node
    {
        public int Data { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node()
        {
            Left = null;
            Right = null;
        }

        public Node(int data) : this()
        {
            Data = data;
        }

        public void Display()
        {
            Console.WriteLine(Data);
        }
    }

    public class BinarySearchTree
    {
        public Node Root;
        private int NodeCount;

        public BinarySearchTree()
        {
            Root = null;
            NodeCount = 0;
        }

        public void Insert(int value)
        {
            Node newNode = new Node();
            newNode.Data = value;

            NodeCount++;

            if (Root == null)
            {
                Root = newNode;
                return;
            }

            Node current = Root;
            Node parent;
            while (true)
            {
                parent = current;
                if (value < current.Data)
                {
                    current = current.Left;
                    if (current == null)
                    {
                        parent.Left = newNode;
                        break;
                    }
                }
                else
                {
                    current = current.Right;
                    if (current == null)
                    {
                        parent.Right = newNode;
                        break;
                    }
                }
            }
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

            if (current == null)
                return null;

            while (current.Right != null)
                current = current.Right;

            return current.Data;
        }

        public Node Find(int value)
        {
            Node current = Root;

            while (current != null && current.Data != value)
            {
                if (current.Left != null && value < current.Left.Data)
                    current = current.Left;
                else
                    current = current.Right;
            }

            return current;
        }

        public bool Delete(int value)
        {
            Node current = Root;
            Node parent = Root;
            bool isLeftChild = true;

            while (current != null && current.Data != value)
            {
                parent = current;
                if (current.Left != null && value < current.Left.Data)
                {
                    isLeftChild = true;
                    current = current.Left;
                }
                else
                {
                    isLeftChild = false;
                    current = current.Right;
                }
            }

            if (current == null)    //Not found
                return false;

            //Found. current is the node to be deleted
            if (current.Left == null && current.Right == null) //leaf node
            {
                if (current == Root)
                    Root = null;
                else if (isLeftChild)
                    parent.Left = null;
                else
                    parent.Right = null;
            }
            else if (current.Right == null)  //if node to be deleted(current) has a left child and no right child
            {
                if (current == Root)
                    Root = current.Left;
                else if (isLeftChild)
                    parent.Left = current.Left;
                else
                    parent.Right = current.Left;
            }
            else if (current.Left == null)   //if node to be deleted(current) has a right child and no left child
            {
                if (current == Root)
                    Root = current.Right;
                else if (isLeftChild)
                    parent.Left = current.Right;
                else
                    parent.Right = current.Right;
            }
            else    //if node to be deleted(current) has both children
            {
                Node successor = GetSuccessor(current);
                if (current == Root)
                    Root = successor;
                else if (isLeftChild)
                    parent.Left = successor;
                else
                    parent.Right = successor;

                successor.Left = current.Left;
            }

            NodeCount--;
            return true;
        }

        private Node GetSuccessor(Node nodeToDelete)
        {
            Node successorParent = nodeToDelete;
            Node successor = nodeToDelete;
            //Move to the right child of the node to be deleted.
            //Then find the smallest on this subtree. This node 
            //will take the place of nodeToDelete in the 
            //tree hierarchy.
            Node current = nodeToDelete.Right;
            while (current != null)
            {
                successorParent = successor;
                successor = current;
                current = current.Left;
            }

            if (successor != nodeToDelete.Right)
            {
                //successor parent.left currently points to successor.
                //So we need to reassign that to be successors right node.
                //Although we can't move left anymore, because we're looking
                //for the smallest, successor might still have a valid right
                //node.
                successorParent.Left = successor.Right;
                successor.Right = nodeToDelete.Right;
            }

            return successor;
        }

        public int GetEdgeCount()
        {
            return NodeCount - 1;
        }

        public int GetNodeCount()
        {
            return NodeCount;
        }

        /// <summary>
        /// Write a program that generates 10,000 random integers in the range of
        ///0–9 and store them in a binary search tree. Display a list of each of 
        ///the integers and the number of times they appear in the tree.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="arr"> array of integers iniialised to zero</param>
        public void GetUniqueNodeCounts(Node root, int[] arr)
        {
            if (root != null)
            {
                GetUniqueNodeCounts(root.Left, arr);
                arr[root.Data]++;
                GetUniqueNodeCounts(root.Right, arr);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree nums = new BinarySearchTree();
            nums.Insert(23);
            nums.Insert(45);
            nums.Insert(16);
            nums.Insert(37);
            nums.Insert(3);
            nums.Insert(99);
            nums.Insert(22);
            Console.WriteLine("Inorder traversal: ");
            nums.InOrder(nums.Root);
            Console.WriteLine("Preorder traversal: ");
            nums.PreOrder(nums.Root);
            Console.WriteLine("Postorder traversal: ");
            nums.PostOrder(nums.Root);

            int? min = nums.FindMin();
            int? max = nums.FindMax();

            Node node1 = nums.Find(99);

            nums.Delete(20);
            Console.WriteLine("Inorder traversal: ");
            nums.InOrder(nums.Root);

            BinarySearchTree bst = new BinarySearchTree();
            Random rand = new Random();
            int[] arr = new int[9];
            for (int i = 0; i < 10000; i++)
            {
                int num = rand.Next(0, 9);
                bst.Insert(num);
            }
            bst.GetUniqueNodeCounts(bst.Root, arr);
            Console.WriteLine($"Node Count: {bst.GetNodeCount()}");
            Console.WriteLine($"Edge Count: {bst.GetEdgeCount()}");
        }
    }
}
