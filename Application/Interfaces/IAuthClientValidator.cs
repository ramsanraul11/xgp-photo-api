namespace xgp_photo_api.Application.Interfaces
{
    public interface IAuthClientValidator
    {
        bool Validate(string clientId, string clientSecret);
    }
}
