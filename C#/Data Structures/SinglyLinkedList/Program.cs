using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglyLinkedList
{

    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Link { get; set; }

        public Node()
        {
            Link = null;
        }

        public Node(T data)
        {
            Data = data;
            Link = null;
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
                current = current.Link;
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
                newNode.Link = First;
            First = newNode;
            count++;
        }

        //O(1)
        public void Add(T newItem)
        {
            Node<T> newNode = new Node<T>(newItem);

            if (count == 0)            
                First = newNode;                        
            else if (count == 1)
            {
                Last = newNode;
                First.Link = newNode;
            }
            else
            {
                Last.Link = newNode;
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
                newNode.Link = current.Link;
                current.Link = newNode;
            }
            else
                Add(newItem);
        }        

        //O(n)
        public void Remove(T item)
        {
            if (count == 0)
                return;

            Node<T> current = First;
            Node<T> previous = null;
            dynamic x = current.Data, y = item;
            bool found = false;

            //search and keep track of previous node
            while (current != null)
            {
                if (x == y)
                {
                    found = true;
                    break;
                }
                previous = current;
                current = current.Link;
                if (current != null)
                    x = current.Data;
            }

            if(previous == current) //remove first item
            {
                First = First.Link;
                count--;
            }
            else if(found)
            {
                previous.Link = current.Link;
                count--;
            }            
        }

        //O(n)
        public void PrintList()
        {
            Node<T> current = First;
            while (current != null)
            {
                Console.WriteLine(current.Data);
                current = current.Link;
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
            list.AddAfter(e,c);
            list.PrintList();
            list.Remove(c);
            list.Remove(9);
            list.PrintList();
            list.Remove(d);
            list.PrintList();
            Console.WriteLine(list.Contains(3));
            Console.WriteLine(list.Contains(9));
        }
    }
}
