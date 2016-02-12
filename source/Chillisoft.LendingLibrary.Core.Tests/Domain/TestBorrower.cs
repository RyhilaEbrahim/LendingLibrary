using System;
using Chillisoft.LendingLibrary.Core.Domain;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;

namespace Chillisoft.LendingLibrary.Core.Tests.Domain
{
    [TestFixture]
    public class TestBorrower
    {
        [Test]
        public void Construct()
        {
            Assert.DoesNotThrow(() => new Borrower());
        }

        [TestCase("Id", typeof(int))]
        [TestCase("FirstName", typeof(string))]
        [TestCase("Surname", typeof(string))]
        [TestCase("Email", typeof(string))]
        [TestCase("Title", typeof(Title))]
        public void Borrower_Property_ShouldExist(string propertyName, Type propertyType)
        {
            //---------------Set up test pack-------------------
            var sut = typeof (Borrower);
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            sut.ShouldHaveProperty(propertyName, propertyType);
            //---------------Test Result -----------------------

        }
    }
}
