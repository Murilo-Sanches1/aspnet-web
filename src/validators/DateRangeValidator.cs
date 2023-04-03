using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Ultimate.Validators
{
    public class DateRangeValidator : ValidationAttribute
    {
        public string PropertyName { get; set; }

        public DateRangeValidator(string propertyName)
        {
            this.PropertyName = propertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // + value representa o valor da propriedade onde o attribute está sendo aplicado
            if (value != null)
            {
                DateTime toDate = Convert.ToDateTime(value);

                // + ValidationContext contém informações sobre a propriedade da model, da classe e do objeto

                // + ObjectInstance representa a model class que foi criada no processo de model binding
                // + não da para se pegar os valores diretamente de ObjectInstance porque o tipo é object
                // + ObjectInstance se refere a DummyUser nesse contexto
                // + reflection
                // validationContext.ObjectInstance;

                // + em runtime, ObjectType representa o tipo da Model Class que nesse caso é igual a DummyUser e atráves
                // + da reflection estamos acessando a refêrencia da propriedade em especifico baseado no nome
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(this.PropertyName);
                // + nesse contexto otherProperty representa FromDate

                DateTime fromDate = Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));

                if (fromDate > toDate) return new ValidationResult(ErrorMessage, new string[] { this.PropertyName, validationContext.MemberName });

            }

            return null;
        }
    }
}