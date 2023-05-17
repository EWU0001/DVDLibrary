using System;

namespace DVDLibrary
{
    public class DVDBorrowCount : IComparable<DVDBorrowCount>
    {
        public string? DVDName { get; set; }
        public int Count { get; set; }

        public DVDBorrowCount(string dvdname, int count)
        {
            DVDName = dvdname;
            Count = count;
        }
        public int CompareTo(DVDBorrowCount? other)
        {
            return other!.Count.CompareTo(Count);
        }
    }
}
