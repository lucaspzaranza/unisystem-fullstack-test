namespace unisystem_api.Models.DTOs
{
    public class CompanyDTO
    {
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
    }
}
