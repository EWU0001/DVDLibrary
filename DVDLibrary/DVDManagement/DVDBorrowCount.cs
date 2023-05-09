using System;

namespace DVDLibrary
{
    public class DVDBorrowCount: IComparable<DVDBorrowCount>
    {         
        public string? DVDName { get; set; }
        public int Count { get; set; }

        public int CompareTo(DVDBorrowCount? other)
        {
            return other!.Count.CompareTo(Count);
        }
       
    }
}
