using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using unisystem_api.Models.DTOs;

namespace unisystem_api.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; } = 0;
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string Neighborhood { get; set; } = string.Empty;
        public int UF { get; set; } = 0;
        public string City { get; set; } = string.Empty;
        public string Complement { get; set; } = string.Empty;
        public string Cel { get; set; } = string.Empty;
        public string AdminFullName { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public Company()
        {
            
        }

        public Company(CompanyDTO dto)
        {
            UserId = dto.UserId;
            Name = dto.Name;
            Type = dto.Type;
            CNPJ = dto.CNPJ;
            CEP = dto.CEP;
            Address = dto.Address;
            Neighborhood = dto.Neighborhood;
            UF = dto.UF;
            City = dto.City;
            Complement = dto.Complement;
            Cel = dto.Cel;
            AdminFullName = dto.AdminFullName;
            CPF = dto.CPF;
            Email = dto.Email;
        }
    }
}
