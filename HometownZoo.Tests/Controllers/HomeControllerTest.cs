using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HometownZoo;
using HometownZoo.Controllers;

namespace HometownZoo.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index_ReturnsNonNullViewResult()
        {
            // Arrange
            HomeController home = new HomeController();

            // Act
            ViewResult result = home.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About_ReturnsNonNullViewResult()
        {
            HomeController about = new HomeController();
            ViewResult result = about.About() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact_ReturnsNonNullViewResult()
        {
            HomeController contact = new HomeController();
            ViewResult result = contact.Contact() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About_ShouldHaveViewBagMessage()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.About() as ViewResult;

            string message = result.ViewBag.Message;

            Assert.IsNotNull(message);
            Assert.AreNotEqual(string.Empty, message.Trim());
            //Assert.AreEqual("Your application description page.", result.ViewBag.Message);
        }
    }
}
