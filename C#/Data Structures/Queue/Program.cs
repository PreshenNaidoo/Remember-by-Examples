using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{

    /// <summary>
    /// Simple queue class.
    /// Space Complexity O(n)
    /// TimeComplexity:
    /// enQueue O(1)
    /// deQueue O(1)
    /// peek O(1)
    /// </summary>
    public class Queue
    {
        protected ArrayList arrayList;

        public Queue()
        {
            arrayList = new ArrayList();
        }

        public void Enqueue(object item)
        {
            arrayList.Add(item);
        }

        public virtual object Dequeue()
        {
            object temp = arrayList[0];
            arrayList.RemoveAt(0);
            return temp;
        }

        public object Peek()
        {
            return arrayList[0];
        }

        public object Back()
        {
            if (Count() == 0)
                return null;

            return arrayList[arrayList.Count - 1];
        }

        public void ClearQueue()
        {
            arrayList.Clear();
        }

        public int Count()
        {
            return arrayList.Count;
        }

        public bool IsEmpty()
        {
            return Count() == 0;
        }

        public override string ToString()
        {
            if (arrayList.Count > 0)
                return string.Join(",", arrayList.ToArray());

            return string.Empty;
        }
    }

    public struct PriorityQueueItem
    {
        public int Priority;
        public string Name;
    }
    public class PriorityQueue : Queue
    {
        public PriorityQueue() : base()
        {
        }

        //public override object Dequeue()
        //{
        //    object[] items;
        //    int min, minindex = -1;
        //    items = this.arrayList.ToArray();
        //    min = ((PriorityQueueItem)items[0]).Priority;

        //    for (int x = 1; x <= items.GetUpperBound(0); x++)
        //    {
        //        if (((PriorityQueueItem)items[x]).Priority < min)
        //        {
        //            min = ((PriorityQueueItem)items[x]).Priority;
        //            minindex = x;
        //        }

        //    }

        //    this.ClearQueue();

        //    for (int x = 0; x <= items.GetUpperBound(0); x++)
        //        if (x != minindex && ((PriorityQueueItem)items[x]).Name != "")
        //            this.Enqueue(items[x]);

        //    return items[minindex];
        //}

        public override object Dequeue()
        {
            if (IsEmpty())
                return null;

            int minIndex = -1;
            int min = ((PriorityQueueItem)arrayList[0]).Priority;
            for (int x = 1; x < arrayList.Count; x++)
            {
                PriorityQueueItem pq = (PriorityQueueItem)arrayList[x];
                if (pq.Priority < min)
                {
                    min = pq.Priority;
                    minIndex = x;
                }
            }

            object temp = arrayList[minIndex];

            arrayList.RemoveAt(minIndex);

            return temp;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach(object item in arrayList)
            {
                PriorityQueueItem pq = (PriorityQueueItem)item;
                if (string.IsNullOrEmpty(pq.Name))
                    str.AppendLine($"null, {pq.Priority}");
                else
                    str.AppendLine($"{pq.Name}, {pq.Priority}");
            }

            return str.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int a = 0, b = 1, c = 2, d = 3, e = 4;
            Queue queue = new Queue();
            queue.Enqueue(a);
            queue.Enqueue(b);
            queue.Enqueue(c);
            Console.WriteLine(queue);
            queue.Dequeue();
            int temp = (int)queue.Peek();
            Console.WriteLine(queue);
            queue.Enqueue(d);
            Console.WriteLine(queue);
            queue.Dequeue();
            temp = (int)queue.Peek();
            queue.Dequeue();
            temp = (int)queue.Peek();
            queue.Enqueue(e);
            temp = (int)queue.Peek();
            Console.WriteLine(queue);
            queue.ClearQueue();
            Console.WriteLine($"{queue.Count()}");

            //Test priority queue
            PriorityQueue erwait = new PriorityQueue();
            PriorityQueueItem[] erPatient = new PriorityQueueItem[4];
            PriorityQueueItem nextPatient;
            erPatient[0].Name = "Joe Smith";
            erPatient[0].Priority = 1;
            erPatient[1].Name = "Mary Brown";
            erPatient[1].Priority = 0;
            erPatient[2].Name = "Sam Jones";
            erPatient[2].Priority = 3;
            for (int x = 0; x <= erPatient.GetUpperBound(0); x++)
                erwait.Enqueue(erPatient[x]);

            nextPatient = (PriorityQueueItem)erwait.Dequeue();
            Console.WriteLine(nextPatient.Name);
            Console.WriteLine(erwait.ToString());

        }
    }
}
