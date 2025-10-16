namespace xgp_photo_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IAuthClientValidator _clientValidator;

        private readonly JwtTokenService _jwt;

        public AuthController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            JwtTokenService jwt,
            IAuthClientValidator clientValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwt = jwt;
            _clientValidator = clientValidator;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            // 1️⃣ Validar client credentials
            if (!_clientValidator.Validate(dto.ClientId, dto.ClientSecret))
                return Unauthorized("Cliente no autorizado.");

            // 2️⃣ Validar usuario
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null) return Unauthorized("Usuario no encontrado");

            var check = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!check.Succeeded) return Unauthorized("Credenciales inválidas");

            var token = await _jwt.CreateTokenAsync(user);
            return Ok(new { Token = token });
        }
    }
}