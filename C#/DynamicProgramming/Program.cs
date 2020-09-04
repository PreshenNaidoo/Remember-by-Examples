using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicProgramming
{
    class Program
    {
        public static int fibonacci(int i)
        {
            if (i == 0) return 0;
            if (i == 1) return 1;
            return fibonacci(i - 1) + fibonacci(i - 2);
        }

        public static int fibonacci(int i, int[] memo)
        {
            if (i == 0) return 0;
            if (i == 1) return 1;

            if (memo[i] == 0)
                memo[i] = fibonacci(i - 1, memo) + fibonacci(i - 2, memo);

            return memo[i];
        }

        public static int fibonacciBottomUp(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;

            int[] memo = new int[n];
            memo[0] = 0;
            memo[1] = 1;
            for (int i = 2; i < n; i++)
            {
                memo[i] = memo[i - 1] + memo[i - 2];
            }

            return memo[n - 1] + memo[n - 2];
        }

        public static int fibonacciInPlace(int n)
        {
            if (n == 0) return 0;
            if (n == 1) return 1;

            int a = 0, b = 1;

            for (int i = 2; i < n; i++)
            {
                int c = a + b;
                a = b;
                b = c;
            }

            return a + b;
        }

        static void Main(string[] args)
        {
            int fib = fibonacci(45);

            int fib1 = fibonacci(45, new int[45+1]);

            int fib2 = fibonacciBottomUp(45);

            int fib3 = fibonacciInPlace(45);
        }
    }
}
