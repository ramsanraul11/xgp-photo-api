namespace xgp_photo_api.Domain.Entities
{
    public class Project
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string BannerClickTitle { get; set; } = default!;
        public string BannerClickDescription { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Relación 1:N con ProjectDetails
        public ICollection<ProjectDetail> Details { get; set; } = new List<ProjectDetail>();
    }
}
