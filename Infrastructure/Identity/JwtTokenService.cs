namespace xgp_photo_api.Infrastructure.Identity
{
    public class JwtTokenService : ITokenService
    {
        private readonly JwtOptions _options;
        private readonly UserManager<IdentityUser> _userManager;

        public JwtTokenService(IOptions<JwtOptions> options, UserManager<IdentityUser> userManager)
        {
            _options = options.Value;
            _userManager = userManager;
        }

        public async Task<string> CreateTokenAsync(IdentityUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new(ClaimTypes.Name, user.UserName ?? "")
        };

            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_options.ExpMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
