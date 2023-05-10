namespace DVDLibrary
{
    public interface IBSTree<T> where T : IComparable //binary search tree interface class
    {
        bool IsEmpty();

        void AddMember(T item);

        void Remove(T item);

        T? Search(T item);

        void TraverseInOrder();

    }
    public class Node
    {
        private IComparable obj; // data
        private Node? left; // Reference to left child 
        private Node? right; // Reference to right child
        public Node(IComparable obj)
        {
            this.obj = obj;
            left = null;
            right = null;
        }
        public IComparable Obj
        {
            get { return obj; }
            set { obj = value; }
        }
        public Node? Left
        {
            get { return left; }
            set { left = value; }
        }
        public Node? Right
        {
            get { return right; }
            set { right = value; }
        }
    }
}