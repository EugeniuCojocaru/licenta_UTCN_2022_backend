using System.ComponentModel.DataAnnotations;

namespace licenta.Models.Syllabuses
{
    public class Section8CreateDto
    {
        [Required(ErrorMessage = "You should provide the teaching methods for courses")]
        public List<string>? TeachingMethodsCourse { get; set; } = new List<string>();
        [Required(ErrorMessage = "You should provide the teaching methods for labs")]
        public List<string>? TeachingMethodsLab { get; set; } = new List<string>();
        [Required(ErrorMessage = "You should provide the course bibligraphy")]
        public List<string>? BibliographyCourse { get; set; } = new List<string>();
        [Required(ErrorMessage = "You should provide the lab bibliography")]
        public List<string>? BibliographyLab { get; set; } = new List<string>();

        [Required(ErrorMessage = "You should provide the course lecures")]
        public List<Section8ElementCreateDto>? LecturesCourse { get; set; } = new List<Section8ElementCreateDto>();
        [Required(ErrorMessage = "You should provide the lab lecures")]
        public List<Section8ElementCreateDto>? LecturesLab { get; set; } = new List<Section8ElementCreateDto>();
    }
}
