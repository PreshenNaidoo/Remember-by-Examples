using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics
{

    //  where T : IComparable   - T must implement IComparable
    //  where T : Produce       - T is of type Product class or its subclasses are allowed only
    //  where T : struct        - T is a value type allowed only
    //  where T : class         - T is a reference type allowed
    //  where T : new()         - T is a type that has a default(parameterless) constructor
    public class Utilities<T> where T : IComparable, new()
    {
        public int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        public T max(T a, T b)
        {
            return a.CompareTo(b) > 0 ? a : b;
        }

        public void DoSomething()
        {
            var temp = new T(); //only possible with the new() constraint
        }
    }    

    public class Nullable<T> where T : struct
    {
        private object _value;

        public Nullable()
        {

        }

        public Nullable(T val)
        {
            _value = val;   //boxing
        }

        public bool HasValue
        {
            get { return _value != null; }
        }

        public T GetValueOrDefault()
        {
            if (HasValue)
                return (T)_value;   //unboxing

            return default(T);      //Default keyword for return the default value of a value data type
        }
    }

    class Program
    {
        public static void foo<T>(T val) where T : struct
        {
            Console.WriteLine($"{val}");
        }

        static void Main(string[] args)
        {
            foo<int>(1);

            Nullable<int> number = new Nullable<int>();
            Console.WriteLine($"Has value: {number.HasValue}");
            Console.WriteLine($"Value: {number.GetValueOrDefault()}");
        }
    }
}
