using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace WorkMate.Models.Validations
{
    public class Currency : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Currency Validation
            // 1) There must be 1 decimal
            // 2) There cannot be more than 1 decimal
            // 3) There must be 2 decimal points

            Regex rgx = new Regex("[0-9]+[.][0-9]{2}$");

            if (rgx.IsMatch(value.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Must be a valid number with two decimal places (eg. 12.34).");
            }
        }
    }
}