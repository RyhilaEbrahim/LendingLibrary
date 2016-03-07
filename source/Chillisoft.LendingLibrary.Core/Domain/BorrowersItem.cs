using System;

namespace Chillisoft.LendingLibrary.Core.Domain
{
    public class BorrowersItem: EntityBase
    {
         public virtual Borrower Borrower { get; set; }
         public virtual Item Item { get; set; }
         public int BorrowerId { get; set; }
         public int ItemId { get; set; }
         public DateTime DateBorrowed { get; set; }
         public DateTime DateReturned{ get; set; }
    }
}