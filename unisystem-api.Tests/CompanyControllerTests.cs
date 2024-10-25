using Xunit;
using Moq;
using unisystem_api.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unisystem_api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using unisystem_api.Models.DTOs;
using unisystem_api.Models;

namespace unisystem_api.Tests
{
    public class CompanyControllerTests
    {
        private readonly Mock<ICompanyService> _mockCompanyService;
        private readonly CompanyController _companyController;

        public CompanyControllerTests()
        {
            _mockCompanyService = new Mock<ICompanyService>();
            _companyController = new CompanyController(_mockCompanyService.Object);
        }

        [Fact]
        public async Task CreateCompany_ReturnsOkResult_WithCreatedAccount()
        {
            // Arrange
            var companyDto = new CompanyDTO { Name = "LPZ Softwares", Email = "lucaszaranza@gmail.com" };
            var expectedCompany = new Company { Id = 1, Name = "LPZ Softwares", Email = "lucaszaranza@gmail.com" };
            _mockCompanyService.Setup(s => s.CreateCompanyAsync(companyDto)).ReturnsAsync(expectedCompany);

            // Act
            var result = await _companyController.CreateCompany(companyDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<Company>(okResult.Value);
            Assert.Equal(expectedCompany.Id, returnValue.Id);
            Assert.Equal(expectedCompany.Name, returnValue.Name);
            Assert.Equal(expectedCompany.Email, returnValue.Email);
        }
    }
}
