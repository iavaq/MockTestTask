using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSVQueries.Controllers;
using NUnit.Framework;
using CSVQueries.Services;
using Moq;

namespace CSVQueriesTests.ControllerTests
{
    public class HomeControllerTests
    {
        private HomeController _controller;
        private Mock<ICSVQueryService> _service;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<ICSVQueryService>();
            _controller = new HomeController(_service.Object);

        }
        //[Test]
        /* public void TestGetHome()
         {
             //arrange
             string expectedContent = "Welcome to the CSV Queries API!";


             //act
             var result = _controller.Get();

             //assert;
             Assert.AreEqual(expectedContent, result);
         }*/
    }
}
