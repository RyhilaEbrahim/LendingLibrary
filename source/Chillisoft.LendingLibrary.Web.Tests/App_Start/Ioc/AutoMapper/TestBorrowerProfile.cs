using AutoMapper;
using Chillisoft.LendingLibrary.Core.Domain;
using Chillisoft.LendingLibrary.Web.Bootstrappers.AutoMapper;
using Chillisoft.LendingLibrary.Web.Models;
using NUnit.Framework;

namespace Chillisoft.LendingLibrary.Web.Tests.Ioc.AutoMapper
{
    [TestFixture]
    public class TestAutoMapperMappings
    {
        [Test]
        public void AutoMapper_ShouldConfigureMappingsCorrectly()
        {
            //---------------Set up test pack-------------------
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<BorrowerProfile>();
                cfg.AddProfile<ItemProfile>();
            });
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            //---------------Test Result -----------------------
            Mapper.AssertConfigurationIsValid();
        }
    }
}