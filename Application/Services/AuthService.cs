namespace xgp_photo_api.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAuthClientValidator _clientValidator;
        private readonly ITokenService _tokenService;

        public AuthService(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IAuthClientValidator clientValidator,
            ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _clientValidator = clientValidator;
            _tokenService = tokenService;
        }

        public async Task<AuthResponseDto?> AuthenticateAsync(LoginDto dto)
        {
            if (!_clientValidator.Validate(dto.ClientId, dto.ClientSecret))
                return null;

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return null;

            var token = await _tokenService.CreateTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthResponseDto
            {
                Token = token,
                //Email = user.Email!,
                //Roles = roles
            };
        }
    }
}
