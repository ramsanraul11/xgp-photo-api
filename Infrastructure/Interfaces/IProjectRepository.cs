namespace xgp_photo_api.Infrastructure.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetAllAsync();
    }
}
