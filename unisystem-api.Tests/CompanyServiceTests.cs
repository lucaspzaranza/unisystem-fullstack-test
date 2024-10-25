using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using unisystem_api.Data;
using unisystem_api.Services;
using unisystem_api.Models.DTOs;
using System.Security.Principal;
using unisystem_api.Models;

namespace unisystem_api.Tests
{
    public class CompanyServiceTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly CompanyService _companyService;

        public CompanyServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDb")
           .Options;

            _dbContext = new ApplicationDbContext(options);
            _companyService = new CompanyService(_dbContext);
        }

        [Fact]
        public async Task AddCompanyAsync_ShouldAddAccountAndReturnIt()
        {
            // Arrange
            var companyDto = new CompanyDTO { Name = "LPZ Softwares", Email = "lucaszaranza@gmail.com" };

            // Act
            var company = await _companyService.CreateCompanyAsync(companyDto);

            // Assert
            Assert.NotNull(company);
            Assert.Equal(companyDto.Name, company.Name);
            Assert.Equal(companyDto.Email, company.Email);
            Assert.True(company.Id > 0);
        }

        [Fact]
        public async Task GetCompanyByIdAsync_ShouldReturCompany_WhenCompanyExists()
        {
            // Arrange
            var company = new Company { Name = "LPZ Softwares", Email = "lucaszaranza@gmail.com" };
            _dbContext.Companies.Add(company);
            await _dbContext.SaveChangesAsync();

            // Act
            var retrievedAccount = await _companyService.GetCompanyByIdAsync(company.Id);

            // Assert
            Assert.NotNull(retrievedAccount);
            Assert.Equal(company.Id, retrievedAccount.Id);
            Assert.Equal(company.Name, retrievedAccount.Name);
            Assert.Equal(company.Email, retrievedAccount.Email);
        }

        [Fact]
        public async Task GetCompanyByIdAsync_ShouldReturnNull_WhenCompanyDoesNotExist()
        {
            // Act
            var account = await _companyService.GetCompanyByIdAsync(99);

            // Assert
            Assert.Null(account);
        }
    }
}
