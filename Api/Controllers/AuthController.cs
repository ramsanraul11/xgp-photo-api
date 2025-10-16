namespace xgp_photo_api.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Inicia sesión y devuelve un token JWT válido.
        /// </summary>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AuthenticateAsync(dto);
            if (result == null)
            {
                _logger.LogWarning("Intento de login fallido para {Email}", dto.Email);
                return Unauthorized("Credenciales inválidas o cliente no autorizado.");
            }

            _logger.LogInformation("Usuario {Email} autenticado correctamente.", dto.Email);
            return Ok(result);
        }
    }
}