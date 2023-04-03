using System.ComponentModel.DataAnnotations;

namespace Ultimate.Validators
{
    public class CustomAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // + value = value que vem da request
            if (value == null)
            {
                return new ValidationResult("Preencha seu nome 😡");
            }

            if ((string)value != "murilo")
            {
                return new ValidationResult("Seu nome não é murilo 😡");
            }

            return ValidationResult.Success;
        }
    }

    public class AllowOnlyName : ValidationAttribute
    {
        public string Name { get; set; } = "";
        public string DefaultErrorMessage { get; set; } = "Seu nome não é {0} 😡 vindo do overload";

        // + parameterless constructor
        public AllowOnlyName() { }

        public AllowOnlyName(string name) { this.Name = name; }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // + value = value que vem da request

            if ((string)value! != Name)
            {
                return new ValidationResult(string.Format(ErrorMessage
                    ?? DefaultErrorMessage, this.Name));
            }

            return ValidationResult.Success;
        }
    }
}