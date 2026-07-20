using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
namespace TraineeManagement.api.Attributes;
public class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly string[] _extensions;

    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions.Select(x => x.ToLowerInvariant()).ToArray();
    }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_extensions.Contains(extension))
            {
                return new ValidationResult($"This file extension is not allowed. Choose from: {string.Join(", ", _extensions)}");
            }
        }

        return ValidationResult.Success!;
    }
}