using System.Collections.Generic;

namespace Chillisoft.LendingLibrary.Core.Domain
{
    public class Item: EntityBase
    {
         public string Description { get; set; }
         public virtual ICollection<BorrowersItem> BorrowersItems { get; set; }
    }
}