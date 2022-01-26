using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.API.Models
{
    public class CourseForCreationDto : CourseForManipulationDto
    {
        [MaxLength(1500, ErrorMessage = "The description shouldn't have more than 1500 characters.")]
        [Required(ErrorMessage = "You should fill out a description.")]
        public string Description { get; set; }
    }
}