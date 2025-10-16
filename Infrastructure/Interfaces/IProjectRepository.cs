namespace xgp_photo_api.Infrastructure.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
        Task<Project?> GetByIdAsync(Guid id);
        Task AddAsync(Project project);
        Task UpdateAsync(Project project);
        Task SaveChangesAsync();
    }
}
