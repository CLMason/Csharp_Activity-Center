using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




public class FutureDateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if((DateTime)value<DateTime.Now)
            return new ValidationResult("Date must be in the Future");

        return ValidationResult.Success;
    }
}
