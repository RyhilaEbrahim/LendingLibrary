﻿using System.Web.Mvc;
using Chillisoft.LendingLibrary.Web.Controllers;
using NUnit.Framework;

namespace Chillisoft.LendingLibrary.Web.Tests.Controllers
{
    [TestFixture]
    public class TestHomeController
    {
        [Test]
        public void Index_ShouldReturnViewResult()
        {
            //---------------Set up test pack-------------------
            var controller = new HomeController();  
            //---------------Assert Precondition----------------

            //---------------Execute Test ----------------------
            var result = controller.Index() as ViewResult;
            //---------------Test Result -----------------------
            Assert.IsNotNull(result);
        }
    }
}