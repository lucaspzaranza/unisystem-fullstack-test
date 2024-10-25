using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using unisystem_api.Data;
using unisystem_api.Models;
using unisystem_api.Models.DTOs;
using unisystem_api.Services.Interfaces;

namespace unisystem_api.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext _context;

        public CompanyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _context.Companies.AsNoTracking().ToListAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _context.Companies.FindAsync(id);
        }

        public async Task<Company?> GetCompanyByUserIdAsync(string userId)
        {
            return await _context.Companies.FirstOrDefaultAsync(comp => comp.UserId == userId);
        }

        public async Task<Company> CreateCompanyAsync(CompanyDTO companyDTO)
        {
            Company company = new Company(companyDTO);

            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<Company?> UpdateCompanyAsync(Company company)
        {
            var foundCompany = await GetCompanyByIdAsync(company.Id);
            if (foundCompany == null)
                return null;

            var trackedEntity = _context.Companies.Local.FirstOrDefault(comp => comp.Id == company.Id);
            if (trackedEntity == null)
                _context.Companies.Update(company);
            else
                _context.Entry(trackedEntity).CurrentValues.SetValues(company);

            await _context.SaveChangesAsync();
            return company;
        }

        public async Task<bool> DeleteCompanyByIdAsync(int id)
        {
            var company = await GetCompanyByIdAsync(id);
            if (company == null) return false;

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
