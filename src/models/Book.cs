using System.ComponentModel.DataAnnotations;
using Ultimate.Validators;

namespace Ultimate.Models
{
    public class Book
    {
        [Required(ErrorMessage = "{0} tem que ser preenchido")]
        [Display(Name = "Book Identifier")]
        public int? BookId { get; set; }

        [Required(ErrorMessage = "O nome do autor não pode ser vazio ou nulo")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "{0} deve ter entre {2} e {1} caracteres")]
        [CustomAttribute]
        [AllowOnlyName("murilo", ErrorMessage = "erro {0}")]
        [Display(Name = "Título")]
        public string? Name { get; set; }

        public override string ToString()
        {
            return $"Book ID = {BookId}, Name = {Name}";
        }
    }
}