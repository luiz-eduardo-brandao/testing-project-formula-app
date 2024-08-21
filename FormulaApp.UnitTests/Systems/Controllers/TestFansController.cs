using FluentAssertions;
using FormulaApp.Api.Controllers;
using FormulaApp.Api.Models;
using FormulaApp.Api.Services.Interfaces;
using FormulaApp.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FormulaApp.UnitTests.Systems.Controllers
{
    public class TestFansController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnStatusCode200() 
        {
            // Arrange -> Set up what your test will need to use
            var mockFanService = new Mock<IFanService>();

            mockFanService.Setup(service => service.GetAllFans())
                .ReturnsAsync(FansFixtures.GetFans());
                
            var fansController = new FansController(mockFanService.Object);

            // Act -> Executing the action
            var result = (OkObjectResult) await fansController.Get();

            // Assert -> Getting the result and making sure that it is the result I need
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_OnSuccess_InvokeFanService() 
        {
            // Arrange
            var mockFanService = new Mock<IFanService>();

            mockFanService.Setup(service => service.GetAllFans())
                .ReturnsAsync(FansFixtures.GetFans());
                
            var fansController = new FansController(mockFanService.Object);

            // Act
            var result = (OkObjectResult) await fansController.Get();

            // Assert
            mockFanService.Verify(service => service.GetAllFans(), Times.Once);
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnListOfFans() 
        {            
            // Arrange
            var mockFanService = new Mock<IFanService>();

            mockFanService.Setup(service => service.GetAllFans())
                .ReturnsAsync(FansFixtures.GetFans());
                
            var fansController = new FansController(mockFanService.Object);

            // Act
            var result = (OkObjectResult) await fansController.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.Value.Should().BeOfType<List<Fan>>();
        }

        [Fact]
        public async Task Get_OnNoFans_ReturnNotFound() 
        {
            // Arrange
            var mockFanService = new Mock<IFanService>();

            mockFanService.Setup(service => service.GetAllFans())
                .ReturnsAsync(new List<Fan>());
                
            var fansController = new FansController(mockFanService.Object);

            // Act
            var result = (NotFoundResult) await fansController.Get();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}