using System.Collections;
using unisystem_api.Models;
using unisystem_api.Models.DTOs;

namespace unisystem_api.Services.Interfaces
{
    public interface ICompanyService
    {
        public Task<IEnumerable<Company>> GetCompaniesAsync();
        public Task<Company?> GetCompanyByIdAsync(int id);
        public Task<Company?> GetCompanyByUserIdAsync(string userId);
        public Task<Company> CreateCompanyAsync(CompanyDTO Company);
        public Task<Company?> UpdateCompanyAsync(Company Company);
        public Task<bool> DeleteCompanyByIdAsync(int id);
    }
}
