using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{

    public class TableValue
    {
        public int Key { get; set; }
        public int? Value { get; set; }
    }

    public class HashTable
    {
        //Choose a prime number that a good size initially and doesn't take up too much memory until required.
        //A good starting size is 10007
        int _arraySize = 11;//10007;
        int _elementCount;
        TableValue[] _dataArray;

        public HashTable()
        {
            _dataArray = new TableValue[_arraySize];
            _elementCount = 0;
        }

        public void Add(int key, int value)
        {
            //TODO, change to dynamically expand
            if (_elementCount == _arraySize)
                return;

            int index;
            if (Hash(key, out index))
            {
                TableValue tv = new TableValue() { Key = key, Value = value };
                _dataArray[index] = tv;
                _elementCount++;
            }
        }

        public void Remove(int key)
        {
            int index;
            if (Hash(key, out index))
            {
                _dataArray[index] = null;
                _elementCount--;
            }
        }

        public bool ContainsKey(int key)
        {
            int index;
            if (Hash(key, out index))
            {
                if (_dataArray[index] != null)
                    return true;
            }

            return false;
        }

        private bool Hash(int key, out int index)
        {
            bool found = true;
            index = key % _arraySize;
            if (_dataArray[index] != null && _dataArray[index].Key != key)
            {
                //Collision hit
                //Resolve collision by quadratic probing
                int count = 1;
                int stopCount = 0;
                found = false;

                while (stopCount < _arraySize / 2)
                {
                    int rehash = index + (count * count);
                    if (rehash >= _arraySize)
                        rehash = rehash % _arraySize; //wrap around

                    if (_dataArray[rehash] == null || (_dataArray[rehash]!=null && _dataArray[rehash].Key == key))
                    {
                        index = rehash;
                        found = true;
                        break;
                    }

                    stopCount++;
                    count++;
                }
            }

            return found;
        }

        public int? GetValue(int key)
        {
            int index;
            if (Hash(key, out index))
            {
                return _dataArray[index].Value;
            }

            return null;
        }

        public int Count
        {
            get
            {
                return _elementCount;
            }
        }

        public int? this[int key]
        {
            get
            {
                return GetValue(key);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HashTable ht = new HashTable();
            ht.Add(31, 31);
            ht.Add(19, 19);
            ht.Add(2, 2);
            ht.Add(13, 13);
            ht.Add(25, 25);
            ht.Add(24, 24);
            ht.Add(21, 21);
            ht.Add(9, 9);

            int? val = ht[25];
        }
    }
}
