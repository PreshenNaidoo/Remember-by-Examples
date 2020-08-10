using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoublyLinkedList
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> FLink { get; set; }
        public Node<T> BLink { get; set; }

        public Node()
        {
            FLink = null;
            BLink = null;
        }

        public Node(T data)
        {
            Data = data;
            FLink = null;
            BLink = null;
        }
    }

    public class LinkedList<T>
    {
        private Node<T> First;
        private Node<T> Last;
        private int count;

        public LinkedList()
        {
            First = null;
            Last = null;
            count = 0;
        }

        //O(n)
        private Node<T> Find(T item)
        {
            Node<T> current = First;

            dynamic x = current.Data, y = item;
            while (current != null && x != y)
            {
                current = current.FLink;
                if (current != null)
                    x = current.Data;
            }

            return current;
        }

        //O(1)
        public void AddFirst(T newItem)
        {
            Node<T> newNode = new Node<T>(newItem);
            if (First != null)
            {
                newNode.FLink = First;
                First.BLink = newNode;
            }
            First = newNode;
            count++;
        }

        //O(1)
        public void Add(T newItem)
        {
            Node<T> newNode = new Node<T>(newItem);

            if (count == 0)
            {
                First = newNode;
                Last = First;
            }            
            if (count == 1)
            {
                Last = newNode;
                First.FLink = newNode;
                newNode.BLink = First;
            }
            else
            {
                Last.FLink = newNode;
                newNode.BLink = Last;
                Last = newNode;
            }

            count++;
        }

        //O(n)
        public void AddAfter(T newItem, T after)
        {
            Node<T> newNode = new Node<T>(newItem);
            Node<T> current = Find(after);
            if (current != null)
            {
                newNode.FLink = current.FLink;
                newNode.BLink = current;
                if(newNode.FLink!=null)
                    newNode.FLink.BLink = newNode;
                current.FLink = newNode;
            }
            else //add at end
                Add(newItem);
        }
        
        //O(n)
        public void Remove(T data)
        {
            if (count == 0)
                return;

            Node<T> temp = Find(data);
            if (temp == null) //not found
                return;

            if (temp.BLink == null) //remove first
            {
                First = temp.FLink;
                First.BLink = null;                
            }
            else if (temp.FLink == null) //remove last
            {
                temp.BLink.FLink = null;
                Last = temp.BLink;
            }
            else
            {
                temp.BLink.FLink = temp.FLink;
                temp.FLink.BLink = temp.BLink;
            }

            count--;
        }

        //O(n)
        public void PrintList()
        {
            Node<T> current = First;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.FLink;
            }
        }

        //O(n)
        public void PrintReverseList()
        {
            Node<T> current = Last;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.BLink;
            }
        }

        //O(1)
        public int Count
        {
            get
            {
                return count;
            }
        }

        //O(n)
        public bool Contains(T item)
        {
            Node<T> node = Find(item);
            if (node == null)
                return false;
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int a = 0, b = 1, c = 2, d = 3, e = 4;
            LinkedList<int> list = new LinkedList<int>();
            list.AddFirst(a);
            list.Add(b);
            list.Add(c);
            list.Add(d);
            list.AddAfter(e, c);
            list.PrintList();
            Console.WriteLine();
            list.Remove(c);
            list.Remove(9);
            list.PrintList();
            Console.WriteLine();
            list.PrintReverseList();
            Console.WriteLine();
            Console.WriteLine(list.Contains(3));
            Console.WriteLine(list.Contains(9));
        }
    }
}



