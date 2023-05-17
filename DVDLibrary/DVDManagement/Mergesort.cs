using System;

namespace DVDLibrary
{
    public class Mergesort<T> where T : IComparable<T> //dynamic mergesort class
    {
        public static List<T> Sort(List<T> list) //using merge sort to sort array of borrwing
        {
            MergeSort(list, 0, list.Count - 1);
            return list;
        }
        public static void MergeSort(List<T> list, int left, int right)
        {
            if (left < right)
            {
                int middle = (left + right) / 2;
                MergeSort(list, left, middle);
                MergeSort(list, middle + 1, right);
                Merge(list, left, middle, right);
            }
        }
        private static void Merge(List<T> list, int left, int middle, int right)
        {
            int n1 = middle - left + 1;
            int n2 = right - middle;

            List<T> leftList = new List<T>(n1);
            List<T> rightList = new List<T>(n2);

            for (int i = 0; i < n1; i++)
                leftList.Add(list[left + i]);
            for (int j = 0; j < n2; j++)
                rightList.Add(list[middle + 1 + j]);

            int x = 0;
            int y = 0;
            int k = left;

            while (x < n1 && y < n2)
            {
                if (leftList[x].CompareTo(rightList[y]) <= 0)
                {
                    list[k] = leftList[x];
                    x++;
                }
                else
                {
                    list[k] = rightList[y];
                    y++;
                }
                k++;
            }

            while (x < n1)
            {
                list[k] = leftList[x];
                x++;
                k++;
            }

            while (y < n2)
            {
                list[k] = rightList[y];
                y++;
                k++;
            }
        }
    }
}
