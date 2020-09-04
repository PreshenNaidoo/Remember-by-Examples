using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRUcache
{

    public class LRUcache
    {
        LinkedList<int> _dList;
        Dictionary<int, LinkedListNode<int>> _indexNodes;
        int _capacity;

        public LRUcache(int capacity)
        {
            _capacity = capacity;
            _dList = new LinkedList<int>();
            _indexNodes = new Dictionary<int, LinkedListNode<int>>();
        }

        public int get(int key)
        {
            if (_indexNodes.ContainsKey(key))
            {
                LinkedListNode<int> node = _indexNodes[key];
                return node.Value;
            }

            return -1;
        }

        public void put(int key, int value)
        {
            if (_indexNodes.ContainsKey(key))
            {
                LinkedListNode<int> node = _indexNodes[key];
                _dList.Remove(node);
                _dList.AddFirst(node);
            }
            else
            {
                if (_dList.Count == _capacity)
                    _dList.RemoveLast();

                LinkedListNode<int> node = _dList.AddFirst(value);
                _indexNodes.Add(key, node);
            }
        }

        public void print()
        {
            foreach (int val in _dList)
            {
                Console.WriteLine(val);
            }
        }

        public void clear()
        {
            _dList.Clear();
            _indexNodes.Clear();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LRUcache lru = new LRUcache(4);
            lru.put(1, 1);
            lru.put(2, 2);
            lru.put(3, 3);
            lru.put(1, 1);
            lru.put(4, 4);
            lru.put(5, 5);

            lru.print();
        }
    }
}
