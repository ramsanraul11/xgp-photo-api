namespace xgp_photo_api.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _repository.GetAllAsync();
            return projects.Select(MapToDto);
        }

        public async Task<ProjectDto> CreateAsync(ProjectCreateDto dto)
        {
            var project = new Project
            {
                Title = dto.Title,
                Description = dto.Description ?? string.Empty,
                ImageUrl = dto.ImageUrl,
                BannerClickTitle = dto.BannerClickTitle ?? string.Empty,
                BannerClickDescription = dto.BannerClickDescription ?? string.Empty,
                IsActive = dto.IsActive,
                CreateDate = DateTime.UtcNow,
                Details = dto.Details.Select(d => new ProjectDetail
                {
                    ImageUrl = d.ImageUrl,
                    IsActive = d.IsActive,
                    CreateDate = DateTime.UtcNow
                }).ToList()
            };

            await _repository.AddAsync(project);
            await _repository.SaveChangesAsync();

            return MapToDto(project);
        }

        public async Task<ProjectDto?> UpdateAsync(ProjectUpdateDto dto)
        {
            var project = await _repository.GetByIdAsync(dto.Id);
            if (project == null) return null;

            project.Title = dto.Title;
            project.Description = dto.Description ?? string.Empty;
            project.ImageUrl = dto.ImageUrl;
            project.BannerClickTitle = dto.BannerClickTitle ?? string.Empty;
            project.BannerClickDescription = dto.BannerClickDescription ?? string.Empty;
            project.IsActive = dto.IsActive;
            project.ModifiedDate = DateTime.UtcNow;

            // 🔁 Actualizar detalles
            var existingDetails = project.Details.ToList();
            project.Details.Clear();

            foreach (var detailDto in dto.Details)
            {
                var existing = existingDetails.FirstOrDefault(d => d.ImageUrl == detailDto.ImageUrl);
                if (existing != null)
                {
                    existing.IsActive = detailDto.IsActive;
                    existing.ModifiedDate = DateTime.UtcNow;
                    project.Details.Add(existing);
                }
                else
                {
                    project.Details.Add(new ProjectDetail
                    {
                        ImageUrl = detailDto.ImageUrl,
                        IsActive = detailDto.IsActive,
                        CreateDate = DateTime.UtcNow
                    });
                }
            }

            await _repository.UpdateAsync(project);
            await _repository.SaveChangesAsync();

            return MapToDto(project);
        }

        // 🔧 Helper
        private static ProjectDto MapToDto(Project p) => new()
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            ImageUrl = p.ImageUrl,
            IsActive = p.IsActive,
            CreateDate = p.CreateDate,
            Details = p.Details.Select(d => new ProjectDetailDto
            {
                Id = d.Id,
                ImageUrl = d.ImageUrl,
                IsActive = d.IsActive
            }).ToList()
        };
    }
}
