namespace xgp_photo_api.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectDto>> GetAllAsync();
        Task<ProjectDto> CreateAsync(ProjectCreateDto dto);
        Task<ProjectDto?> UpdateAsync(ProjectUpdateDto dto);
    }
}
