using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingAndSearching
{
    class Program
    {
        /// <summary>
        /// Time Complexity O(n^2)
        /// Best Complexity O(n)
        /// Space Complexity O(1)
        /// Stable: Yes
        /// </summary>
        /// <param name="arr"></param>
        static void BubbleSort(int[] arr)
        {
            int n = arr.Length;
            for (int pass = n - 1; pass >= 0; pass--)
            {
                for (int i = 0; i <= pass - 1; i++)
                {
                    if (arr[i] > arr[i + 1])
                    {
                        //swap
                        int temp = arr[i];
                        arr[i] = arr[i + 1];
                        arr[i + 1] = temp;
                    }
                }
            }
        }

        /// <summary>
        /// Time Complexity O(n^2)
        /// Best Complexity O(n^2)
        /// Space Complexity O(1)
        /// Stable: No
        /// </summary>
        /// <param name="arr"></param>
        static void SelectionSort(int[] arr)
        {
            int n = arr.Length;
            int minIndex;
            for (int i = 0; i < n - 1; i++)
            {
                minIndex = i;
                for (int j = i + 1; j < n; j++)
                {
                    if (arr[j] < arr[minIndex])
                        minIndex = j;
                }

                int temp = arr[minIndex];
                arr[minIndex] = arr[i];
                arr[i] = temp;
            }
        }

        /// <summary>
        /// Time Complexity O(n^2)
        /// Best Complexity O(n)
        /// Space Complexity O(1)
        /// Stable: Yes
        /// </summary>
        /// <param name="arr"></param>
        static void InsertionSort(int[] arr)
        {
            int n = arr.Length;
            int val, j;
            for (int i = 1; i <= n - 1; i++)
            {
                val = arr[i];
                j = i;
                while (j >= 1 && arr[j - 1] > val)
                {
                    arr[j] = arr[j - 1];
                    j--;
                }
                arr[j] = val;
            }
        }

        /// <summary>
        /// Time Complexity O(n*log(n))
        /// Best Complexity O(n*log(n))
        /// Space Complexity O(1)
        /// Stable: No
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        static void QuickSort(int[] arr, int low, int high)
        {
            int pivot;
            if (high > low)
            {
                pivot = Partition(arr, low, high);
                QuickSort(arr, low, pivot - 1);
                QuickSort(arr, pivot + 1, high);
            }
        }

        static int Partition(int[] arr, int low, int high)
        {
            int left, right, pivot_item = arr[low];
            left = low;
            right = high;

            while (left < right)
            {
                //move left while item < pivot
                while (arr[left] < pivot_item)
                    left++;
                //move right while item > pivot
                while (arr[right] > pivot_item)
                    right--;
                if (left < right) //swap
                {
                    int temp = arr[left];
                    arr[left] = arr[right];
                    arr[right] = temp;
                }
            }

            //right is final position for the pivot
            int temp1 = arr[left];
            arr[left] = arr[right];
            arr[right] = temp1;
            //arr[low] = arr[right];
            //arr[right] = pivot_item;
            return right;
        }

        /// <summary>
        /// This function takes last element as pivot, places the pivot element at its correct
        /// position in sorted array, and places all smaller(smaller than pivot) to left of
        /// pivot and all greater elements to right of pivot
        /// </summary>
        /// <param name="arr">Array part to be sorted</param>
        /// <param name="low">Starting index</param>
        /// <param name="high">Ending index</param>
        /// <returns></returns>
        static int partition(int[] arr, int low,
                                       int high)
        {
            int pivot = arr[high];

            // index of smaller element 
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                // If current element is smaller  
                // than the pivot 
                if (arr[j] < pivot)
                {
                    i++;

                    // swap arr[i] and arr[j] 
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            // swap arr[i+1] and arr[high] (or pivot) 
            int temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;

            return i + 1;
        }


        /// <summary>
        /// The main function that implements QuickSort()
        /// Time Complexity O(n*log(n))
        /// Best Complexity O(n*log(n))
        /// Space Complexity O(1)
        /// Stable: No
        /// </summary>
        /// <param name="arr">Array to be sorted</param>
        /// <param name="low">Starting index</param>
        /// <param name="high">Ending index</param>
        static void quickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {

                /* pi is partitioning index, arr[pi] is  
                now at right place */
                int pi = partition(arr, low, high);

                // Recursively sort elements before 
                // partition and after partition 
                quickSort(arr, low, pi - 1);
                quickSort(arr, pi + 1, high);
            }
        }

        /// <summary>
        /// Time Complexity O(log(n))        
        /// Space Complexity O(1)        
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        static bool BinarySearch(int[] arr, int low, int high, int searchValue)
        {
            int mid = low + (high - low) / 2;
            if (low > high)
                return false;
            if (arr[mid] == searchValue)
                return true;
            else if (arr[mid] > searchValue)
                return BinarySearch(arr, low, mid - 1, searchValue);
            else
                return BinarySearch(arr, mid + 1, high, searchValue);

            return false;
        }

        static void Main(string[] args)
        {
            int[] arr = { 10, 4, 7, 3, 9, 1, 8, 2, 5 };
            BubbleSort(arr.Clone() as int[]);
            SelectionSort(arr.Clone() as int[]);
            InsertionSort(arr.Clone() as int[]);
            quickSort(arr, 0, arr.Length - 1);

            bool found = BinarySearch(arr, 0, arr.Length - 1, 3);

            Console.WriteLine(string.Join(",", arr));
            Console.Read();
        }
    }
}
