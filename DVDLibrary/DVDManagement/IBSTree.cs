namespace DVDLibrary
{
    public interface IBSTree<T> where T : IComparable //binary search tree interface class
    {
        bool IsEmpty();

        void Insert(T item);

        void Delete(T item);

        T? Search(T item);

        void TraverseInOrder();

        void Clear();
    }
}