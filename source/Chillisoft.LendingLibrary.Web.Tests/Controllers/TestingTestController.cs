using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Chillisoft.LendingLibrary.Web.Controllers;
using NUnit.Framework;

namespace Chillisoft.LendingLibrary.Web.Tests.Controllers
{
    [TestFixture]
    class TestingTestController
    {
        [Test]
        public void Index_GivenAMessageOnViewBag_ShouldReturnMessage()
        {
            //---------------Set up test pack-------------------
            var controller=new TestController();
            //---------------Assert Precondition----------------
            //---------------Execute Test ----------------------
            var viewResult = controller.Index() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(viewResult);
            Assert.AreEqual("Hello",viewResult.ViewBag.Message);
        }
    }
}
