namespace xgp_photo_api.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAllAsync() =>
            await _context.Projects
                .Include(p => p.Details)
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.CreateDate)
                .ToListAsync();

        public async Task<Project?> GetByIdAsync(Guid id) =>
            await _context.Projects
                .Include(p => p.Details)
                .FirstOrDefaultAsync(p => p.Id == id);

        public async Task AddAsync(Project project)
        {
            await _context.Projects.AddAsync(project);
        }

        public async Task UpdateAsync(Project project)
        {
            _context.Projects.Update(project);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
