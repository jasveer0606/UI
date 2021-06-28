using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UI;
using UI.Controllers;

namespace Application.UnitTest
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void ConstructorNotNullTest()
        {
            HomeController homeController = new HomeController(Mock.Of<IConfiguration>(), Mock.Of<ILogger<HomeController>>());
            Assert.IsNotNull(homeController);
        }

        [TestMethod]
        public void EmployeeCreateGetCheckTest()
        {
            HomeController homeController = new HomeController(Mock.Of<IConfiguration>(), Mock.Of<ILogger<HomeController>>());
            ViewResult viewResult = homeController.EmpCreate() as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void EmployeeCreatePostTest()
        {
            HomeController homeController = new HomeController(Mock.Of<IConfiguration>(), Mock.Of<ILogger<HomeController>>());
            Employee employee = Mock.Of<Employee>();
            ViewResult viewResult = homeController.EmpCreate(employee) as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void EmployeeListViewPostCheckTest()
        {
            HomeController homeController = new HomeController(Mock.Of<IConfiguration>(), Mock.Of<ILogger<HomeController>>());
            IFormCollection collection = Mock.Of<IFormCollection>();
            ViewResult viewResult = homeController.EmpListView(collection) as ViewResult;
            Assert.AreEqual(0, viewResult.ViewData.Count);
        }

        [TestMethod]
        public void EmployeeListViewGetCheckTest()
        {
            HomeController homeController = new HomeController(Mock.Of<IConfiguration>(), Mock.Of<ILogger<HomeController>>());
            ViewResult viewResult = homeController.EmpListView() as ViewResult;
            Assert.AreEqual(0, viewResult.ViewData.Count);
        }

        [TestMethod]
        public void EmployeeSalaryGetCheckTest()
        {
            HomeController homeController = new HomeController(Mock.Of<IConfiguration>(), Mock.Of<ILogger<HomeController>>());
            ViewResult viewResult = homeController.EmpSalary() as ViewResult;
            Assert.IsNotNull(viewResult);
        }

        [TestMethod]
        public void EmployeeSalaryPostCheckTest()
        {
            HomeController homeController = new HomeController(Mock.Of<IConfiguration>(), Mock.Of<ILogger<HomeController>>());
            ViewResult viewResult = homeController.EmpSalary(1) as ViewResult;
            Assert.IsNotNull(viewResult);
        }
    }
}
