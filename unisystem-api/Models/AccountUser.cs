using Microsoft.AspNetCore.Identity;

namespace unisystem_api.Models
{
    public class AccountUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
