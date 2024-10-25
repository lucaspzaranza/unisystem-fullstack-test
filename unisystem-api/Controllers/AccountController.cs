using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using unisystem_api.Data;
using unisystem_api.Models;
using unisystem_api.Models.DTOs;
using unisystem_api.Services;
using unisystem_api.Services.Interfaces;

namespace unisystem_api.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AccountUser> _userManager;
        private readonly SignInManager<AccountUser> _signInManager;

        public AccountController(UserManager<AccountUser> userManager, SignInManager<AccountUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private IQueryable<AccountGetDTO> GetUsersAccountGetDTO()
        {
            return _userManager.Users
                .Select(user => new AccountGetDTO
                {
                    FullName = user.FullName,
                    Email = user.Email ?? "",
                    Id = user.Id
                });
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAccountsAsync()
        {
            var users = await GetUsersAccountGetDTO().ToListAsync();

            return Ok(users);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetAllAccountsAsync(string id)
        {
            var user = await GetUsersAccountGetDTO().FirstOrDefaultAsync(user => user.Id == id);
            return Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountCreateDTO account)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new AccountUser { FullName = account.FullName, UserName = account.Email, Email = account.Email };
            var result = await _userManager.CreateAsync(user, account.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            user.Id = await _userManager.GetUserIdAsync(user);

            return Ok(user);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAccount(string id, [FromBody] AccountUpdateDTO account)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound(new { message = "Usuário não encontrado." });

            user.FullName = account.FullName;
            user.Email = account.Email;
            user.UserName = account.Email;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Ok(new { message = "Usuário atualizado com sucesso."});

            return BadRequest(result.Errors);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound(new { message = "Usuário não encontrado." });

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return NoContent();

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);

            if (!result.Succeeded)
                return Unauthorized();

            var user = await GetUsersAccountGetDTO().FirstAsync(user => user.Email == login.Email);

            return Ok(user);
        }
    }
}
