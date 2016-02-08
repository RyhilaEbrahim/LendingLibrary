using System;
using System.CodeDom;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Policy;
using Chillisoft.LendingLibrary.Core.Domain;
using NUnit.Framework;
using Chillisoft.LendingLibrary.Tests.Common.Builders;
using NUnit.Framework.Constraints;
using PeanutButter.TempDb.LocalDb;
using PeanutButter.TestUtils.Generic;

namespace Chillisoft.LendingLibrary.DB.Tests.DomainPersistence
{
    [TestFixture]
    public class TestBorrowerPersistence
    {
        private TempDBLocalDb _localDb;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _localDb = new TempDBLocalDb();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _localDb?.Dispose();
        }

        [Test]
        public void Borrower_ShouldPersistAndRecall()
        {
            //---------------Set up test pack-------------------
            var borrower = new BorrowerBuilder()
                                .WithRandomProps()
                                .WithProp(b => b.Id = 0)
                                .WithValidTitleId()
                                .Build();
            //---------------Assert Precondition----------------

            using (var ctx = new LendingLibraryDbContext(_localDb.CreateConnection()))
            {
                //ctx.Set<Borrower>().Add(borrower);
                //ctx.Set(typeof (Borrower)).Add(borrower);
                //ctx.Set(borrower.GetType()).Add(borrower);
                //ctx.Entry(borrower).State = EntityState.Added;

                ctx.Borrowers.Add(borrower);
                ctx.SaveChanges();
            }

            using (var ctx = new LendingLibraryDbContext(_localDb.CreateConnection()))
            {
                //---------------Execute Test ----------------------
                var result = ctx.Set<Borrower>().Single();
                //---------------Test Result -----------------------
                PropertyAssert.AllPropertiesAreEqual(borrower, result, "Title");
            }

          
        }

    }
}