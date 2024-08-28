using HubWally.Application.DTOs.Accounts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HubWally.Application.Services.IServices
{
    public interface IAuthService
    {
        Task<IdentityResult> Register(RegisterDto model);
        Task<string?> Login(LoginDto model);
    }
}
