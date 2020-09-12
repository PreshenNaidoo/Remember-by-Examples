using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stack
{
    class Program
    {
        /// <summary>
        /// Simple Stack Class
        /// Time Complexity:
        /// Push O(1)
        /// Pop O(1)
        /// Peek O(1)
        /// Space Complexity O(n)
        /// </summary>
        public class Stack
        {
            private int index;
            private ArrayList arrayList;

            public Stack()
            {
                arrayList = new ArrayList();
                index = -1;
            }

            public int Count()
            {
                return arrayList.Count;
            }

            public void Push(object item)
            {
                arrayList.Add(item);
                index++;
            }

            public object Pop()
            {
                if (index < 0)
                    return null;

                object temp = arrayList[index];
                arrayList.RemoveAt(index);
                index--;
                return temp;
            }

            public void Clear()
            {
                arrayList.Clear();
                index = -1;
            }

            public object Peek()
            {
                if (index < 0)
                    return null;

                return arrayList[index];
            }

            public override string ToString()
            {
                if (arrayList.Count > 0)
                    return string.Join(",", arrayList.ToArray());

                return string.Empty;
            }
        }

        static bool IsPalindrome(string word)
        {
            if (string.IsNullOrEmpty(word))
                return false;

            Stack stack = new Stack();
            string lower = word.ToLower();

            foreach (char c in lower)
                stack.Push(c);

            foreach (char c in lower)            
                if (c != (char)stack.Pop())
                    return false;            

            return true;
        }

        static void MulBase(int n, int b)
        {
            Stack Digits = new Stack();
            do
            {
                Digits.Push(n % b);
                n /= b;
            } while (n != 0);
            while (Digits.Count() > 0)
                Console.Write(Digits.Pop());
        }

        static bool SymbolBalance(string str)
        {
            Stack<char> symb = new Stack<char>();

            foreach(char c in str)
            {
                if (c == '(' || c == '[' || c == '{')
                    symb.Push(c);
                else if (c == ')' || c == ']' || c == '}')
                {
                    if (symb.Count() == 0)
                        return false;

                    char open = symb.Pop();
                    if (open == '(' && c != ')')
                        return false;
                    else if (open == '[' && c != ']')
                        return false;
                    else if (open == '{' && c != '}')
                        return false;
                }
            }

            if (symb.Count() > 0)
                return false;

            return true;
        }

        static void Main(string[] args)
        {
            int a = 0, b = 1, c = 2, d = 3, e = 4;

            Stack stack = new Stack();
            stack.Push(a);
            stack.Push(b);
            stack.Push(c);
            Console.WriteLine(stack);
            stack.Pop();
            Console.WriteLine(stack);

            bool test = IsPalindrome("Racecar");
            bool test2 = IsPalindrome("HelloWorld");

            MulBase(8, 2);

            bool check1 = SymbolBalance("(a+b)+(c-d)");
            bool check2 = SymbolBalance("((a+b)+(c-d)");
            bool check3 = SymbolBalance("((a+b)+[c-d])");
            bool check4 = SymbolBalance("((a+b)+[c-d]}");
        }
    }
}
