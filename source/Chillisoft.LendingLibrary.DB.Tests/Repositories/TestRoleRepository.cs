using System;
using System.Linq;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.DB.Repositories;
using Chillisoft.LendingLibrary.Tests.Common.Builders;
using NSubstitute;
using NUnit.Framework;

namespace Chillisoft.LendingLibrary.DB.Tests.Repositories
{
    [TestFixture]
    public class TestRoleRepository
    {
        [Test]
        public void Construct()
        {
            Assert.DoesNotThrow(() => new RoleRepository(Substitute.For<ILendingLibraryDbContext>()));
        }

        [Test]
        public void Construct_GivenLendingLibraryDbContextIsNull_ShouldThrow()
        {
            //---------------Set up test pack-------------------
            
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var exception = Assert.Throws<ArgumentNullException>(() => new RoleRepository(null));
            //---------------Test Result -----------------------
            Assert.AreEqual("lendingLibraryDbContext",exception.ParamName);
        }

        [Test]
        public void GetRoleById_GivenExistingForId_ShouldRole()
        {
            //---------------Set up test pack-------------------
            var roles = new RoleBuilder().WithRandomProps().Build();
            var dbContext = new TestDbContextBuilder().WithRoles(roles).Build();
            var repository = new RoleRepository(dbContext);
            //---------------Assert Precondition---------------
            //---------------Execute Test ----------------------
            var roleById = repository.GetRoleById(roles.Id);
            //---------------Test Result -----------------------
            Assert.AreEqual(roles,roleById);
        }

        [Test]
        public void GetAllRoles_GivenOneRole_ShouldRole()
        {
            //---------------Set up test pack-------------------
            var role = new RoleBuilder().WithRandomProps().Build();
            var dbContext = new TestDbContextBuilder().WithRoles(role).Build();
            var repository = new RoleRepository(dbContext);
            //---------------Assert Precondition---------------
            //---------------Execute Test ----------------------
            var roles = repository.GetAllRoles();
            //---------------Test Result -----------------------
            Assert.AreEqual(1, roles.Count);
            var actual = roles.First();
            Assert.AreSame(roles.FirstOrDefault(), actual);
        }

        [Test]
        public void GetAllRoles_GivenTwoRoles_ShouldRole()
        {
            //---------------Set up test pack-------------------
            var role1 = new RoleBuilder().WithRandomProps().Build();
            var role2 = new RoleBuilder().WithRandomProps().Build();
            var dbContext = new TestDbContextBuilder().WithRoles(role1,role2).Build();
            var repository = new RoleRepository(dbContext);
            //---------------Assert Precondition---------------
            //---------------Execute Test ----------------------
            var roles = repository.GetAllRoles();
            //---------------Test Result -----------------------
            Assert.AreEqual(2, roles.Count);
            var actualFirst = roles.First();
            Assert.AreSame(role1, actualFirst);
            var actualLast = roles.Last();
            Assert.AreSame(role2,actualLast);
        }

        [Test]
        public void GetAllRoles_GivenThreeRoles_ShouldRole()
        {
            //---------------Set up test pack-------------------
            var role1 = new RoleBuilder().WithRandomProps().Build();
            var role2 = new RoleBuilder().WithRandomProps().Build();
            var role3 = new RoleBuilder().WithRandomProps().Build();
            var dbContext = new TestDbContextBuilder().WithRoles(role1,role2,role3).Build();
            var repository = new RoleRepository(dbContext);
            //---------------Assert Precondition---------------
            //---------------Execute Test ----------------------
            var roles = repository.GetAllRoles();
            //---------------Test Result -----------------------
            Assert.AreEqual(3, roles.Count);
            var actualFirst = roles.First();
            Assert.AreSame(role1, actualFirst);
            var actualLast = roles.Last();
            Assert.AreSame(role3,actualLast);
        }

    }
}