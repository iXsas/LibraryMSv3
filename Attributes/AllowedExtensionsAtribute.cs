using System.ComponentModel.DataAnnotations;

namespace LibraryMSv3.Attributes
{
    public class AllowedExtensionsAtribute: ValidationAttribute
    {
        private readonly string[] _allowedExtensions;
        public AllowedExtensionsAtribute(string[] allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName);
                if (!_allowedExtensions.Contains(extension))
                {
                    return new ValidationResult("This file extension isn't supported");
                }
            }
            return ValidationResult.Success;
        }
    }
}

