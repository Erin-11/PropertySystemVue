using KnightFrank.BAL.Attributes;
using KnightFrank.DataAccessLayer.EF.Common;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace KnightFrank.BAL.Extensions
{
    public static class DictionaryExtension
    {
        internal static IEnumerable<FilterColumn> AsFilterColumnEnumerable<TDto>(this IDictionary<string, string> filterColumns, LogicalOperatorEnum logicalOperator, string filterValue = null)
        {
            var type = typeof(TDto);

            List<FilterColumn> _filterColumns = new List<FilterColumn>();

            return filterColumns.Aggregate(_filterColumns, (current, filter) =>
            {
                var propInfo = type.GetProperties().SingleOrDefault(e => e.Name.ToLower() == filter.Key.ToLower());
                if (propInfo != null)
                {
                    var customAttritubes = propInfo.GetCustomAttributes();
                    if (customAttritubes != null)
                    {
                        var typeAttr = customAttritubes.SingleOrDefault(e => typeof(KeywordSearchAttribute) == e.GetType());
                        var value = string.IsNullOrEmpty(filterValue) ? filter.Value : filterValue;

                        if (typeAttr == null)
                        {
                            current.Add(new FilterColumn(propInfo.Name, value, logicalOperator));
                        }
                        else
                        {
                            var attr = typeAttr as KeywordSearchAttribute;
                            if (attr.AllowKeywordSearch && attr.Key.ToLower() == filter.Key.ToLower())
                            {
                                current.Add(new FilterColumn(attr.Key, value, logicalOperator));
                            }
                        }
                    }
                }

                return current;
            });
        }
    }
}
