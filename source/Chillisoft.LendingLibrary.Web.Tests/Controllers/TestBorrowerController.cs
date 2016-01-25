using Chillisoft.LendingLibrary.Web.Controllers;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Chillisoft.LendingLibrary.Web.Tests.Controllers
{
    [TestFixture]
    public class TestBorrowerController
    {
        [Test]
        public void Construct()
        {
            Assert.DoesNotThrow(() => new BorrowerController());
        }
    }
}