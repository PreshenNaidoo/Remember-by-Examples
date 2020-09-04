using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopExample
{
    class Program
    {
        public class One
        {
            public void Hello()
            {
                Console.WriteLine("One.Hello");
            }

            public virtual void Hello1()
            {
                Console.WriteLine("One.Hello1");
            }
        }

        public class Two : One
        {
            //No override here. Static Polymorphism/compile time polymorphism
            public void Hello()
            {
                Console.WriteLine("Two.Hello");
            }

            //Dynamic polymorphism/runtime polymorphism
            public override void Hello1()
            {
                Console.WriteLine("Two.Hello1");
            }
        }

        public abstract class Shape
        {
            public abstract void Draw();

            //Default Implementation
            public virtual double Area()
            {
                Console.WriteLine("In Shape.Area");
                return 0;
            }
        }

        public class Rectangle : Shape
        {
            //Must be implemented
            public override void Draw()
            {
                Console.WriteLine("In Rectangle.Draw");                
            }
        }

        static void Main(string[] args)
        {
            One one = new Two();
            one.Hello();
            one.Hello1();

            Shape shape = new Rectangle();
            shape.Draw();
            double area = shape.Area();
        }
    }
}
