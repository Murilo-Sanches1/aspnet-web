using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Ultimate.Validators;

namespace Ultimate.Models
{
    public class DummyUser : IValidatableObject
    {
        public string? Name { get; set; }
        public DateTime? FromDate { get; set; }
        [DateRangeValidator("FromDate", ErrorMessage = "From Date deve ser maior ou igual a To Date")]
        public DateTime? ToDate { get; set; }
        public string? Email { get; set; }
        public int? Phone { get; set; }
        [BindNever]
        public string? Role { get; set; }



        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Email == null && this.Phone.HasValue == false)
            {
                yield return new ValidationResult(
                    "Email ou número de telefone deve ser preenchido",
                    new[] { nameof(Email) });
            }

            if (this.Email?.Trim().Length <= 5 && this.Phone <= 18)
            {
                yield return new ValidationResult(
                    "Preencha seu nome completo e idade deve ser maior que 18",
                    new[] { nameof(Email), nameof(Phone) });
            }
        }
    }
}