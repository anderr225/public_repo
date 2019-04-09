using System;
using Moq;
using WebApiCFT.Controllers;
using WebApiCFT.Models;
using Xunit;

namespace TestCft
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var context = new Mock<CftContext>();
            context.Setup(c => c.Projects.FindAsync(5)).ReturnsAsync(new Project{Id = 5});
            var controller = new ProjectsController(context.Object);

            var project = controller.Get(5);
            
            Assert.Equal(5, project.Id);
        }
    }
}