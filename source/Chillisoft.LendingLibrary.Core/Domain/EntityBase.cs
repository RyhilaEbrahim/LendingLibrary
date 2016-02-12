using System;

namespace Chillisoft.LendingLibrary.Core.Domain
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        public bool IsNew()
        {
            if (Id < 0)
            {
                throw new InvalidOperationException("Id cannot be less than 0");
            }
            return Id == default(int);
        }
    }
}