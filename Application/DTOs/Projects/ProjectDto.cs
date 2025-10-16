namespace xgp_photo_api.Application.DTOs.Projects
{
    public class ProjectDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }

        public List<ProjectDetailDto> Details { get; set; } = new();
    }

    public class ProjectDetailDto
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
