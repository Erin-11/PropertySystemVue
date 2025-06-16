using System;
using System.ComponentModel.DataAnnotations;

namespace KnightFrank.BAL.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class KeywordSearchAttribute: ValidationAttribute
    {
        private readonly string _key;
        private readonly bool _allowKeywordSearch;
        private const string _defaultError = "'{0}' not allow for keyword search.";

        public KeywordSearchAttribute(bool allowed = true)
            : this(null, allowed) { }

        public KeywordSearchAttribute(string key, bool allowed = true)
            : base(_defaultError)
        {
            _key = key;
            _allowKeywordSearch = allowed;
        }

        public string Key { get { return _key; } }

        public bool AllowKeywordSearch { get { return _allowKeywordSearch; } }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            return _allowKeywordSearch;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(ErrorMessageString, name);
        }
    }
}
