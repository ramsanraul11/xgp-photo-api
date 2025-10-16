namespace xgp_photo_api.Models
{
    public class AuthClient
    {
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
