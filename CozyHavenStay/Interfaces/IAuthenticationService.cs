using CozyHavenStay.Models.DTOs;

namespace CozyHavenStay.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse> Login(UserLoginRequest loginRequest);
    }
}
