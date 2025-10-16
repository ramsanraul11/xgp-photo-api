namespace xgp_photo_api.Services
{
    public interface IAuthClientValidator
    {
        bool Validate(string clientId, string clientSecret);
    }

    public class AuthClientValidator : IAuthClientValidator
    {
        private readonly List<AuthClient> _clients;
        public AuthClientValidator(IOptions<List<AuthClient>> options)
        {
            _clients = options.Value;
        }

        public bool Validate(string clientId, string clientSecret)
        {
            return _clients.Any(c =>
                c.ClientId == clientId &&
                c.ClientSecret == clientSecret);
        }
    }
}
