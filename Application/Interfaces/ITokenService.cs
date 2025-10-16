namespace xgp_photo_api.Application.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateTokenAsync(IdentityUser user);
    }
}
