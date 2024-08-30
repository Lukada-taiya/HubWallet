using HubWally.Application.Commands.Requests.Accounts;
using HubWally.Application.Services.IServices;

namespace HubWally.Application.Commands.RequestHandlers.Accounts
{
    public class RegisterCommandHandler(IAuthService service) : IRequestHandler<RegisterCommand, ApiResponse>
    {
        private readonly IAuthService _service = service;
        public async Task<ApiResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //TODO: Add validation, data annotations?
                var response = await _service.Register(request.registerDto);
                if (response.Succeeded)
                    return new()
                    {
                        Success = true,
                        Message = "Registration successful",
                        Body = response,
                    };

                throw new Exception("Registration failed");
            }
            catch (Exception ex)
            {
                return new()
                {
                    Success = false,
                    Message = ex.Message ?? "Something went wrong. Try again later."
                };
            }
        }
    }
}
