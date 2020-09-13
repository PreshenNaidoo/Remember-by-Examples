using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaExpressions
{
    //What is a lambda expression?
    //It's just an anonymous method:
    //.No access modifier
    //.No Name
    //.No return statement

    class Program
    {
        static int Square(int side)
        {
            return side * side;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Square(5));

            //Or Using Lambda:
            //args => expression
            //means args goes to expression
            //number => number*number
            //() => expression      -for no arguments
            //x =>  expression      -for one argument you dont need parentheses
            //(x,y,z) => expression -for multiple arguments you need parentheses
            //Scope - The lamba expression scope is where the expression is defined. In the case below
            //the scope is within the main method. So it has access to other variables declared in
            //this method.

            Func<int, int> squaref = Square;
            Func<int, int> squaref1 = number => number * number;

            Console.WriteLine(squaref(5));
            Console.WriteLine(squaref1(5));

            //further examples
            List<int> integers = new List<int>();
            integers.Add(10);
            integers.Add(11);
            integers.Add(12);
            integers.Add(1);

            int integerLess = integers.Find(NumberLessThanTen); //Has a Predicate parameter which is actually a delegate so the value should be a function
            //Or we could simple use a lambda expression(anonymous function):
            int integerLessThanTen = integers.Find(x => x < 10);
        }

        static bool NumberLessThanTen(int num)
        {
            return num < 10;
        }
    }
}
