﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcFakes;
using System;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Mwh.Sample.WebApi.Controllers
    {
    [TestClass]
    public class HomeControllerTest : IDisposable
        {
        private HomeController controller;


        [TestInitialize]
        public void TestInitialize()
            {
            controller = new HomeController();
            var sessionItems = new SessionStateItemCollection();
            sessionItems["item1"] = "wow!";
            controller.ControllerContext = new FakeControllerContext(controller, sessionItems);
            }


        [TestMethod]
        public void EmpSinglePageStateUnderTestExpectedBehavior()
            {
            // Arrange

            // Act
            var result = controller.EmpSinglePage();

            // Assert
            Assert.IsNotNull(result);
            }


        [TestMethod]
        public void Index()
            {
            // Arrange

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result?.ViewBag?.Title);
            }


        #region IDisposable Support
        private bool disposedValue; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
            {
            if (!disposedValue)
                {
                if (disposing)
                    {
                    controller.Dispose();
                    }
                disposedValue = true;
                }
            }

        ~HomeControllerTest()
            {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(false);
            }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
            {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            GC.SuppressFinalize(this);
            }
        #endregion
        }
    }
