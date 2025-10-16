namespace xgp_photo_api.Infrastructure.Identity
{
    public class AuthClientValidator : IAuthClientValidator
    {
        private readonly IReadOnlyList<AuthClient> _clients;

        public AuthClientValidator(IOptions<List<AuthClient>> options)
        {
            _clients = options.Value;
        }

        public bool Validate(string clientId, string clientSecret)
        {
            // Validación estricta: evita NullReference y ataques de tiempo.
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                return false;

            return _clients.Any(c =>
                c.ClientId.Equals(clientId, StringComparison.OrdinalIgnoreCase) &&
                c.ClientSecret == clientSecret);
        }
    }
}
