using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using unisystem_api.Models;
using unisystem_api.Models.DTOs;
using unisystem_api.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace unisystem_api.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public class CompanyController : ControllerBase
    {
        private ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _companyService.GetCompaniesAsync();
            return Ok(companies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyByID(int id)
        {
            var company = await _companyService.GetCompanyByIdAsync(id);

            if(company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpGet("user-id/{userId}")]
        public async Task<IActionResult> GetCompanyByUserIdAsync(string userId)
        {
            var company = await _companyService.GetCompanyByUserIdAsync(userId);

            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateCompany([FromBody] CompanyDTO company)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdCompany = await _companyService.CreateCompanyAsync(company);

            if (createdCompany == null)
                return BadRequest("Não foi possível criar a sua empresa com os dados fornecidos.");

            return Ok(createdCompany);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCompany([FromBody] Company company)
        {
            var updatedCompanyData = await _companyService.UpdateCompanyAsync(company);

            if (updatedCompanyData != null)
                return Ok(updatedCompanyData);
            else
                return NotFound(new { message = "Empresa não encontrada." });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            var result = await _companyService.DeleteCompanyByIdAsync(id);

            if (result)
                return NoContent();
            else
                return NotFound(new { message = "Empresa não encontrada." });
        }
    }
}
