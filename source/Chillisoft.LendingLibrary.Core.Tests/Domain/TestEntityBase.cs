using System;
using Chillisoft.LendingLibrary.Core.Domain;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Chillisoft.LendingLibrary.Core.Tests.Domain
{
    [TestFixture]
    public class TestEntityBase
    {
        [Test]
        public void Construct()
        {
            Assert.DoesNotThrow(() => new EntityBaseImpl());
        }

        [Test]
        public void IsNew_GivenIdNotSet_ShouldReturnTrue()
        {
            //---------------Set up test pack-------------------
            var entityBaseImpl = new EntityBaseImpl();
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var isNew = entityBaseImpl.IsNew();
            //---------------Test Result -----------------------
            Assert.IsTrue(isNew);
        }

        [Test]
        public void IsNew_GivenIdIsSet_ShouldReturnFalse()
        {
            //---------------Set up test pack-------------------
            var entityBaseImpl = new EntityBaseImpl();
            entityBaseImpl.Id = 1;
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var isNew = entityBaseImpl.IsNew();
            //---------------Test Result -----------------------
            Assert.IsFalse(isNew);
        }

        [Test]
        public void IsNew_GivenIdIsLessThan0_ShouldThrow()
        {
            //---------------Set up test pack-------------------
            var entityBaseImpl = new EntityBaseImpl();
            entityBaseImpl.Id = -11;
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var ex = Assert.Throws<InvalidOperationException>(() => entityBaseImpl.IsNew());
            //---------------Test Result -----------------------
            Assert.AreEqual("Id cannot be less than 0", ex.Message);
        }

        public class EntityBaseImpl : EntityBase
        {

        }
    }
}