using Ultimate.Validators;

namespace Ultimate.Models
{
    public class DummyUser
    {
        public string? Name { get; set; }

        public DateTime? FromDate { get; set; }

        [DateRangeValidator("FromDate", ErrorMessage = "From Date deve ser maior ou igual a To Date")]
        public DateTime? ToDate { get; set; }
    }
}