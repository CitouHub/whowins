using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VemVinner.Domain.Validation
{
    public class ListValidator : ValidationAttribute
    {
        public short MinimumCount { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var list = ((IEnumerable)value).Cast<object>().ToList();
            if (list.Count >= MinimumCount)
            {
                return null;
            }

            return new ValidationResult(ErrorMessage, new[] { validationContext.MemberName });
        }
    }
}
