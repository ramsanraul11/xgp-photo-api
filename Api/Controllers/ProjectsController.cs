namespace xgp_photo_api.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _service;
        private readonly ILogger<ProjectsController> _logger;

        public ProjectsController(IProjectService service, ILogger<ProjectsController> logger)
        {
            _service = service;
            _logger = logger;
        }

        /// <summary>
        /// Obtener todos los proyectos activos.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Obtener proyecto por id.
        /// </summary>
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);

            if (result is null)
                return NotFound(new { message = $"No se encontró el proyecto con Id {id}" });

            return Ok(result);
        }
        /// <summary>
        /// Crear un nuevo proyecto.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProjectCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await _service.CreateAsync(dto);
            _logger.LogInformation("Proyecto {Title} creado correctamente.", dto.Title);
            return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
        }

        /// <summary>
        /// Actualizar un proyecto existente (incluyendo sus detalles).
        /// </summary>
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ProjectUpdateDto dto)
        {
            if (id != dto.Id)
                return BadRequest("El ID del cuerpo no coincide con la URL.");

            var result = await _service.UpdateAsync(dto);
            if (result == null)
                return NotFound("Proyecto no encontrado.");

            _logger.LogInformation("Proyecto {Id} actualizado correctamente.", id);
            return Ok(result);
        }
    }
}
