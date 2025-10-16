namespace xgp_photo_api.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Project> Projects => Set<Project>();
        public DbSet<ProjectDetail> ProjectDetails => Set<ProjectDetail>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Project>(entity =>
            {
                entity.ToTable("Projects");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Title).IsRequired().HasMaxLength(200);
                entity.Property(p => p.Description).HasMaxLength(1000);
                entity.Property(p => p.ImageUrl).HasMaxLength(500);

                entity.HasMany(p => p.Details)
                      .WithOne(d => d.Project)
                      .HasForeignKey(d => d.ProjectId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<ProjectDetail>(entity =>
            {
                entity.ToTable("ProjectDetails");
                entity.HasKey(d => d.Id);
                entity.Property(d => d.ImageUrl).IsRequired().HasMaxLength(500);
            });
        }
    }
}
