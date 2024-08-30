using HubWally.Application.DTOs.Accounts;
using Microsoft.AspNetCore.Identity; 

namespace HubWally.Application.Services.IServices
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(RegisterDto model);
        Task<string?> Login(LoginDto model);
    }
}
