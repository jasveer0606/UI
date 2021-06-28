using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UI;

namespace Application.UnitTest
{
    [TestClass]
    public class StartupTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            Startup startup = new Startup(Mock.Of<IConfiguration>());
            Assert.IsNotNull(startup);
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void ConfigureServicesTest()
        {
            Startup startup = new Startup(Mock.Of<IConfiguration>());
            startup.ConfigureServices(Mock.Of<IServiceCollection>());
        }

        [TestMethod]
        [ExpectedException(typeof(System.NullReferenceException))]
        public void ConfigureTest()
        {
            Startup startup = new Startup(Mock.Of<IConfiguration>());
            startup.Configure(Mock.Of<IApplicationBuilder>(), Mock.Of<IWebHostEnvironment>());
        }
    }
}
