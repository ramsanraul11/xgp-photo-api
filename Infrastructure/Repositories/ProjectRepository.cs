namespace xgp_photo_api.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _context.Projects
                .Include(p => p.Details)
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.CreateDate)
                .ToListAsync();
        }
    }
}
