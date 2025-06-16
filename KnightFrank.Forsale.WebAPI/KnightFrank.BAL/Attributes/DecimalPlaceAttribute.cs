using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace KnightFrank.BAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DecimalPlaceAttribute : ValidationAttribute
    {
        private readonly int _decimalPlace;
        private readonly bool _allowEmptyStringValues;
        private readonly bool _required;
        private const string _defaultError = "'{0}' must have at least {1} decimal place.";

        public DecimalPlaceAttribute() : this(2) { }

        public DecimalPlaceAttribute(int decimalPlace, bool required = true, bool allowEmptyStringValues = false)
            : base(_defaultError)
        {
            _decimalPlace = decimalPlace;
            _required = required;
            _allowEmptyStringValues = allowEmptyStringValues;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return !_required;

            if (!_allowEmptyStringValues && value is decimal decimalValue)
                return Regex.IsMatch(value.ToString(), @"^[0-9]{5}\.[0-9]{2}$");

            return false;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name, _decimalPlace);
        }
    }
}
