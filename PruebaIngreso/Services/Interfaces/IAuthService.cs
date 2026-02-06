using PruebaIngreso.DTOs;

namespace PruebaIngreso.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
    }
}
