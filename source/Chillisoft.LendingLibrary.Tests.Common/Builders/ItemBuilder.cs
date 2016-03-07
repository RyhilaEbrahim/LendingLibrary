using Chillisoft.LendingLibrary.Core.Domain;
using PeanutButter.RandomGenerators;

namespace Chillisoft.LendingLibrary.Tests.Common.Builders
{
    public class ItemBuilder : GenericBuilder<ItemBuilder, Item>
    {

        public ItemBuilder WithNewId()
        {
            this.WithProp(item => item.Id = 0);
            return this;
        }
    }
}