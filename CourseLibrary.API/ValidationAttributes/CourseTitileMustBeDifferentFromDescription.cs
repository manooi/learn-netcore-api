using CourseLibrary.API.Models;
using System.ComponentModel.DataAnnotations;

namespace CourseLibrary.API.ValidationAttributes
{
    public class CourseTitileMustBeDifferentFromDescription : ValidationAttribute
    {
        protected override ValidationResult IsValid(
            object value, // object to validate e.g. our Courses
            ValidationContext validationContext)
        {
            var course = (CourseForManipulationDto)validationContext.ObjectInstance;
            if (course.Title == course.Description)
            {
                return new ValidationResult(ErrorMessage, new[] { nameof(CourseForManipulationDto) });
                //return new ValidationResult("The provided description should be different from the title.", new[] { "CourseForCreationDto" });
            }
            return ValidationResult.Success;

        }

    }
}
