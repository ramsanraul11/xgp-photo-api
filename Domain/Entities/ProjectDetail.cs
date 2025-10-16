namespace xgp_photo_api.Domain.Entities
{
    public class ProjectDetail
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ProjectId { get; set; }
        public string ImageUrl { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDate { get; set; }

        // Relación inversa
        public Project Project { get; set; } = default!;
    }
}
