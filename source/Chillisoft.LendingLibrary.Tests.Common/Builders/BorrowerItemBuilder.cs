using System;
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
        public BorrowerItemBuilder WithDateReturnedAsNull()
        {
            this.WithProp(borrower => borrower.DateReturned = "0001 - 01 - 01");
            return this;
        }
        public BorrowerItemBuilder WithFirstName(string firstname)
        {
            this.WithProp(borrower => borrower.Borrower.FirstName = firstname);
            return this;
        }
        public BorrowerItemBuilder WithSurname(string surname)
        {
            this.WithProp(borrower => borrower.Borrower.Surname = surname);
            return this;
        }
        public BorrowerItemBuilder WithPhoneNumber(string phoneNumber)
        {
            this.WithProp(borrower => borrower.Borrower.ContactNumber = phoneNumber);
            return this;
        }
        public BorrowerItemBuilder WithEmail(string email)
        {
            this.WithProp(borrower => borrower.Borrower.Email = email);
            return this;
        }
        public BorrowerItemBuilder WithTitleId(int id)
        {
            this.WithProp(borrower => borrower.Borrower.TitleId = id);
            return this;
        }
        public BorrowerItemBuilder WithItemId(int id)
        {
            this.WithProp(borrower => borrower.ItemId = id);
            return this;
        }
        public BorrowerItemBuilder WithDateBorrowed(DateTime date)
        {
            this.WithProp(borrower => borrower.DateBorrowed = date);
            return this;
        }
        public BorrowerItemBuilder WithBorrowerId(int id)
        {
            this.WithProp(borrower => borrower.BorrowerId = id);
            return this;
        }
        public BorrowerItemBuilder WithBorrowerItemId(int id)
        {
            this.WithProp(borrower => borrower.Id = id);
            return this;
        }
    }
}