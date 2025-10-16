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

            return projects.Select(p => new ProjectDto
            {
                Id = p.Id,
                Title = p.Title,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                IsActive = p.IsActive,
                CreateDate = p.CreateDate,
                Details = p.Details
                    .Where(d => d.IsActive)
                    .Select(d => new ProjectDetailDto
                    {
                        Id = d.Id,
                        ImageUrl = d.ImageUrl,
                        IsActive = d.IsActive
                    }).ToList()
            });
        }
    }
}
