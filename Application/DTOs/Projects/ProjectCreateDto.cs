namespace xgp_photo_api.Application.DTOs.Projects
{
    public class ProjectCreateDto
    {
        [Required, MaxLength(200)]
        public string Title { get; set; } = default!;

        [MaxLength(1000)]
        public string? Description { get; set; }

        [Required, MaxLength(500)]
        public string ImageUrl { get; set; } = default!;

        [MaxLength(200)]
        public string? BannerClickTitle { get; set; }

        [MaxLength(1000)]
        public string? BannerClickDescription { get; set; }

        public bool IsActive { get; set; } = true;

        public List<ProjectDetailCreateDto> Details { get; set; } = new();
    }
    public class ProjectDetailCreateDto
    {
        [Required, MaxLength(500)]
        public string ImageUrl { get; set; } = default!;

        public bool IsActive { get; set; } = true;
    }

    public class ProjectUpdateDto : ProjectCreateDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
