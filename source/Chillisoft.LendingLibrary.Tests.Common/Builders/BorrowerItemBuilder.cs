using Chillisoft.LendingLibrary.Core.Domain;
using PeanutButter.RandomGenerators;

namespace Chillisoft.LendingLibrary.Tests.Common.Builders
{
    public class BorrowerItemBuilder : GenericBuilder<BorrowerItemBuilder, BorrowersItem>
    {
        public BorrowerItemBuilder WithNewId()
        {
            this.WithProp(borrower => borrower.Id = 0);
            return this;
        }
    }
}