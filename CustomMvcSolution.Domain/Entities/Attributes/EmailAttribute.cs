using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using CustomMvcSolution.Resource;

namespace CustomMvcSolution.Domain.Entities.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EmailAttribute : ValidationAttribute
    {
        private static readonly Regex EmailRegex = new Regex(".+@.+\\..+");

        public EmailAttribute()
        {
            ErrorMessage = Resources.InvalidEmailFormat;
        }

        public override Boolean IsValid(Object value)
        {
            return !string.IsNullOrEmpty((string)value) && EmailRegex.IsMatch((string)value);
        }
    }
}
