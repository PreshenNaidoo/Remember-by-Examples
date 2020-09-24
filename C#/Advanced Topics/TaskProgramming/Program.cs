using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProgramming
{
    class Program
    {

        public static void Write(char c)
        {
            int i = 1000;
            while(i-- > 0)
            {
                Console.Write(c);
            }
        }

        public static void Write(object o)
        {
            int i = 1000;
            while (i-- > 0)
            {
                Console.Write(o);
            }
        }

        public static int TextLength(object o)
        {
            Console.WriteLine($"\nTask with id {Task.CurrentId} processing object {o} ...");
            return o.ToString().Length;
        }

        /// <summary>
        /// Creating a Task
        /// </summary>
        public static void test1()
        {
            //Here you're making a task and starting it:            
            Task.Factory.StartNew(() => Write('.'));
            //Here you make a task but have to start it manually.
            var t = new Task(() => Write('?'));
            t.Start();
        }

        /// <summary>
        /// Passing an object paramteer
        /// </summary>
        public static void test2()
        {
            //Passing an object parameter.
            //Note, boxing and unboxing will affect performance.
            Task t = new Task(Write, "hello");
            t.Start();
            Task.Factory.StartNew(Write, 123);
        }

        /// <summary>
        /// Returning a value
        /// </summary>
        public static void test3()
        {
            string text1 = "testing", text2 = "this";
            var task1 = new Task<int>(TextLength, text1);
            task1.Start();
            Task<int> task2 = Task.Factory.StartNew(TextLength, text2);

            //Getting the result of the task is a blocking operation.
            //Asking for result here will wait for the task to complete.
            Console.WriteLine($"Length of '{text1}' is {task1.Result}");
            Console.WriteLine($"Length of '{text2}' is {task2.Result}");
        }

        /// <summary>
        /// Cancellation of tasks
        /// </summary>
        public static void test4()
        {
            //Cancelling tasks.
            //Lets create a task that will run to infinity.

            var cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;

            //If you need to invoke an event when a task gets cancelled
            //so it can be handled accordingly:
            token.Register(() =>
            {
                Console.WriteLine("Cancellation has been requested.");
            });

            Task t = new Task(() =>
            {
                int i = 0;
                while (true)
                {
                    //if (token.IsCancellationRequested)
                    //{
                    //    //using break is more of a soft exit
                    //    //break;
                    //    //The recommended way:
                    //    throw new OperationCanceledException();
                    //}

                    //OR use this:
                    token.ThrowIfCancellationRequested();

                    Console.WriteLine($"{i++}\t");
                }
            }, token);
            t.Start();

            Task.Factory.StartNew(() =>
            {
                token.WaitHandle.WaitOne();
                Console.WriteLine("Wait handle has been released, cancellation was requested.");
            });

            Console.ReadKey();
            cts.Cancel();
        }

        /// <summary>
        /// Composite task cancellations
        /// </summary>
        public static void test5()
        {
            //Composite Cancellation Tokens:
            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var para = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preventative.Token, emergency.Token);

            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    para.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}\t");
                    Thread.Sleep(1000);
                }
            }, para.Token);

            //This will cancel all since the three are linked.
            Console.ReadKey();
            emergency.Cancel();
        }

        /// <summary>
        /// Waiting for time to pass inside a task
        /// </summary>
        public static void test6()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;
            var t = new Task(() =>
            {
                Console.WriteLine("Press key to disarm, you have 5 seconds");
                bool cancelled = token.WaitHandle.WaitOne(5000);
                if (cancelled)
                    Console.WriteLine("Disarmed in time.");
                else
                    Console.WriteLine("Explode.");

            }, token);

            t.Start();

            Console.ReadKey();
            cts.Cancel();
        }

        //Waiting for tasks
        public static void test7()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t1 = new Task(() =>
            {
                Console.WriteLine("Take 5 seconds");
                for (int i = 0; i < 5; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Thread.Sleep(1000);
                }
            }, token);

            t1.Start();

            Task t2 = Task.Factory.StartNew(() => Thread.Sleep(3000), token);

            //Task.WaitAll(t1, t2);                 //Waits for all to complete
            //Task.WaitAny(t1, t2);                   //Waits for whichever one finishes first
            //Task.WaitAny(new[] { t1, t2 }, 4000);   // wait 4 seconds so it would only wait for t2 but not t1

            Task.WaitAll(new[] { t1, t2 }, 4000, token);

            Console.WriteLine($"Task t1 status is {t1.Status}");
            Console.WriteLine($"Task t2 status is {t2.Status}");
        }

        static void Main(string[] args)
        {
            //test1();
            //test2();
            //test3();
            //test4();
            //test5();
            //test6();
            //test7();
            

            Console.WriteLine("Main program done.");
            Console.ReadKey();
        }
    }
}
