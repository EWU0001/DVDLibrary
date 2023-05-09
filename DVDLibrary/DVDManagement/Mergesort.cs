using System;

namespace DVDLibrary
{
    public class Mergesort<T> where T : IComparable<T> //dynamic mergesort class
    {
        public static T[] Sort(T[]arr) //using merge sort to sort array of borrwing
        {
            MergeSort(arr, 0, arr.Length-1);
            return arr;
        }
        public static void MergeSort(T[]arr ,int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSort(arr, left, middle);
                MergeSort(arr, middle + 1, right);
                Merge(arr, left, middle, right);
            }
        }

        private static void Merge(T[]arr ,int left, int middle, int right)
        {
            T[] tempArray = new T[arr.Length];
            int i = left;
            int j = middle + 1;
            int k = left;

            while (i <= middle && j <= right)
            {
                if (arr[i].CompareTo(arr[j]) <= 0)
                {
                    tempArray[k] = arr[i];
                    i++;
                }
                else
                {
                    tempArray[k] = arr[j];
                    j++;
                }
                k++;
            }

            while (i <= middle)
            {
                tempArray[k] = arr[i];
                i++;
                k++;
            }

            while (j <= right)
            {
                tempArray[k] = arr[j];
                j++;
                k++;
            }

            for (k = left; k <= right; k++)
            {
                arr[k] = tempArray[k];
            }
        }
    }
}
