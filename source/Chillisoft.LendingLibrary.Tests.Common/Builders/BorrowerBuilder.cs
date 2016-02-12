using Chillisoft.LendingLibrary.Core.Domain;
using PeanutButter.RandomGenerators;

namespace Chillisoft.LendingLibrary.Tests.Common.Builders
{
    public class BorrowerBuilder: GenericBuilder<BorrowerBuilder, Borrower>
    {
        private int _titleId;
        private bool _isNew;
        public BorrowerBuilder AsNewClient()
        {
            _isNew = true;
            return this;
        }
        public BorrowerBuilder WithValidTitleId()
        {
            _titleId = RandomValueGen.GetRandomInt(1,7);
            return this;
        }

        public override Borrower Build()
        {
            if (_isNew) this.WithProp(borrower => borrower.Id = 0);
            this.WithProp(borrower => borrower.TitleId = _titleId);
            return base.Build();
        }

        public BorrowerBuilder WithNewId()
        {
            this.WithProp(borrower => borrower.Id = 0);
            return this;
        }
        public BorrowerBuilder WithValidExistingId()
        {
            this.WithProp(borrower => borrower.Id = RandomValueGen.GetRandomInt(1,1000));
            return this;
        }
    }

}