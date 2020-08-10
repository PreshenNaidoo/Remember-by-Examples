using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringVsStringBuilderTest
{
    class Program
    {
        static void BuildSB(int size)
        {
            StringBuilder sbObject = new StringBuilder();   //mutable
            for (int i = 0; i <= size; i++)
                sbObject.Append("a");
        }

        static void BuildString(int size)
        {
            string stringObject = "";                       //immutable
            for (int i = 0; i <= size; i++)
                stringObject += "a";
        }

        static void Main(string[] args)
        {
            int size = 100;
            Stopwatch timeSB = new Stopwatch();
            Stopwatch timeST = new Stopwatch();
            Console.WriteLine();
            for (int i = 0; i <= 5; i++)
            {
                timeSB.Start();
                BuildSB(size);                
                timeSB.Stop();
                Console.WriteLine($"Time (in milliseconds) to build StringBuilder object for {size} elements: {timeSB.ElapsedMilliseconds}");
                timeST.Start();
                BuildString(size);
                timeST.Stop();                
                Console.WriteLine($"Time (in milliseconds) to build String object for {size} elements: {timeST.ElapsedMilliseconds} ") ;
                Console.WriteLine();
                size *= 10;
                timeSB.Reset();
                timeST.Reset();
            }

            Console.Read();
        }
    }
}
