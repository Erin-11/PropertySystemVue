using KnightFrank.DataAccessLayer.EF.Common;
using KnightFrank.DataAccessLayer.EF.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace KnightFrank.BAL.Extensions
{
    public static class QueryExtension
    {
        internal static Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> ComposeOrdering<TEntity>(this IEnumerable<SortColumn> sorting)
        {
            return query =>
            {
                if (sorting.Any())
                {
                    IOrderedEnumerable<TEntity> oe = null;
                    var pis = typeof(TEntity).GetProperties();
                    var piCol = pis.SingleOrDefault(c => c.Name.ToLower() == sorting.First().SortColumnName.ToLower());

                    if (piCol != null)
                    {
                        switch (sorting.First().SortDirection)
                        {
                            case SortDirectionEnum.Ascending:
                                oe = query.AsEnumerable().OrderBy(sorting.First().SortColumnName);

                                if (piCol != null)
                                {
                                    if (piCol.PropertyType.FullName == typeof(string).FullName)
                                    {
                                        oe = query.AsEnumerable().OrderBy(sorting.First().SortColumnName, new AlphanumComparator());
                                    }
                                }
                                break;
                            case SortDirectionEnum.Descending:
                                oe = query.AsEnumerable().OrderByDescending(sorting.First().SortColumnName);

                                if (piCol != null)
                                {
                                    if (piCol.PropertyType.FullName == typeof(string).FullName)
                                    {
                                        oe = query.AsEnumerable().OrderByDescending(sorting.First().SortColumnName, new AlphanumComparator());
                                    }
                                }
                                break;
                        }
                    }

                    if (oe != null)
                    {
                        _ = sorting.Skip(1).Aggregate(oe, (current, sort) =>
                        {
                            switch (sort.SortDirection)
                            {
                                case SortDirectionEnum.Ascending:
                                    current.ThenBy(sort.SortColumnName);

                                    if (piCol != null)
                                    {
                                        if (piCol.PropertyType.FullName == typeof(string).FullName)
                                        {
                                            current.ThenBy(sort.SortColumnName, new AlphanumComparator());
                                        }
                                    }
                                    break;
                                case SortDirectionEnum.Descending:
                                    current.ThenByDescending(sort.SortColumnName);

                                    if (piCol != null)
                                    {
                                        if (piCol.PropertyType.FullName == typeof(string).FullName)
                                        {
                                            current.ThenByDescending(sort.SortColumnName, new AlphanumComparator());
                                        }
                                    }
                                    break;
                            }
                            return current;
                        });
                        return oe.AsQueryable().OrderBy(o => true);
                    }
                }
                return query.OrderBy(o => true);
            };
        }

        internal static Expression<Func<TEntity, bool>> ComposeFiltering<TEntity>(this IEnumerable<FilterColumn> filtering)
        {
            Expression<Func<TEntity, bool>> exp = null;
            return filtering.Aggregate(exp, (current, filter) =>
            {
                if (!string.IsNullOrEmpty(filter.FilterColumnName) || !string.IsNullOrEmpty(filter.FilterText))
                {
                    exp = null;
                    Expression value = null;
                    Expression propertyExp = null;

                    var parameterExp = Expression.Parameter(typeof(TEntity), "type");
                    var checkValue = filter.FilterText;

                    if (filter.FilterColumnName.Contains('.'))
                    {
                        propertyExp = filter.FilterColumnName.Split(".").Aggregate(propertyExp, (currentExpression, name) =>
                        {
                            if (currentExpression != null)
                            {
                                if (currentExpression.Type.GetProperty(name) != null)
                                {
                                    return Expression.Property(currentExpression, name);
                                }
                            }

                            if (parameterExp.Type.GetProperty(name) != null)
                            {
                                return Expression.Property(parameterExp, name);
                            }

                            return propertyExp;
                        });
                    }
                    else
                    {
                        if (parameterExp.Type.GetProperty(filter.FilterColumnName) != null)
                        {
                            propertyExp = Expression.Property(parameterExp, filter.FilterColumnName);
                        }
                    }

                    if (propertyExp != null)
                    {
                        if (propertyExp.Type.Name == typeof(string).Name)
                        {
                            value = Expression.Constant(Convert.ChangeType(checkValue, propertyExp.Type), propertyExp.Type);

                            var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            var checkNullMethod = typeof(string).GetMethod("IsNullOrEmpty", new[] { typeof(string) });

                            var valueToLowerExp = Expression.Call(value, toLowerMethod);
                            var toLowerExp = Expression.Call(propertyExp, toLowerMethod);
                            var containsMethodExp = Expression.Call(toLowerExp, containsMethod, valueToLowerExp);
                            var checkNullMethodExp = Expression.Not(Expression.Call(checkNullMethod, propertyExp));

                            var methodExp = Expression.AndAlso(checkNullMethodExp, containsMethodExp);
                            exp = Expression.Lambda<Func<TEntity, bool>>(methodExp, parameterExp);
                        }
                        else if (propertyExp.Type.Name == typeof(bool).Name)
                        {
                            if (checkValue.ToLower() == "yes" || checkValue.ToLower() == "true" || checkValue.ToLower() == "no" || checkValue.ToLower() == "false")
                            {
                                checkValue = checkValue.ToLower() == "yes" || checkValue.ToLower() == "true" ? bool.TrueString : bool.FalseString;

                                if (bool.TryParse(checkValue, out bool resultBoolean))
                                    value = Expression.Constant(resultBoolean, propertyExp.Type);

                                var methodExp = Expression.Equal(propertyExp, value);
                                exp = Expression.Lambda<Func<TEntity, bool>>(methodExp, parameterExp);
                            }
                        }
                        else if (propertyExp.Type.IsEnum)
                        {
                            var fields = propertyExp.Type.GetFields();
                            var filterFields = fields.Where(f => f.CustomAttributes.Any() && f.CustomAttributes.Any(c =>
                                c.ConstructorArguments.Count > 0 && c.ConstructorArguments.Any(a => a.Value.ToString().ToLower().Contains(checkValue.ToString().ToLower()))));
                            exp = filterFields.Aggregate(exp, (c, f) =>
                            {
                                value = Expression.Constant(Convert.ChangeType(f.GetValue(f.Name), propertyExp.Type), propertyExp.Type);

                                var methodExp = Expression.Equal(propertyExp, value);
                                exp = Expression.Lambda<Func<TEntity, bool>>(methodExp, parameterExp);

                                if (c != null)
                                {
                                    if (exp != null)
                                    {
                                        return c.Or(exp);
                                    }

                                    return c;
                                }

                                return exp;
                            });

                            if (exp == null)
                            {
                                var enumValues = propertyExp.Type.GetEnumValues();
                                foreach (var enumValue in enumValues)
                                {
                                    if (enumValue.ToString().ToLower().Contains(checkValue.ToLower()))
                                    {
                                        value = Expression.Constant(Convert.ChangeType(enumValue, propertyExp.Type), propertyExp.Type);

                                        var methodExp = Expression.Equal(propertyExp, value);
                                        var tExp = Expression.Lambda<Func<TEntity, bool>>(methodExp, parameterExp);
                                        exp = exp == null ? exp = tExp : exp.Or(tExp);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var isEnumableValue = propertyExp.Type.IsGenericType && propertyExp.Type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
                            var isNullableValue = propertyExp.Type.IsGenericType && propertyExp.Type.GetGenericTypeDefinition() == typeof(Nullable<>);
                            var valuetype = propertyExp.Type;
                            var typecode = Type.GetTypeCode(propertyExp.Type);

                            if (isNullableValue || isEnumableValue)
                            {
                                var generictype = propertyExp.Type.GetGenericArguments().FirstOrDefault();
                                if (generictype != null)
                                {
                                    typecode = Type.GetTypeCode(generictype);
                                    valuetype = typeof(object);
                                }
                            }

                            switch (typecode)
                            {
                                case TypeCode.Char:
                                    if (char.TryParse(checkValue, out char resultChar))
                                        value = Expression.Constant(resultChar, valuetype);
                                    break;
                                case TypeCode.SByte:
                                    if (sbyte.TryParse(checkValue, out sbyte resultSByte))
                                        value = Expression.Constant(resultSByte, valuetype);
                                    break;
                                case TypeCode.Byte:
                                    if (byte.TryParse(checkValue, out byte resultByte))
                                        value = Expression.Constant(resultByte, valuetype);
                                    break;
                                case TypeCode.Int16:
                                    if (short.TryParse(checkValue, out short resultInt16))
                                        value = Expression.Constant(resultInt16, valuetype);
                                    break;
                                case TypeCode.UInt16:
                                    if (ushort.TryParse(checkValue, out ushort resultUInt16))
                                        value = Expression.Constant(resultUInt16, valuetype);
                                    break;
                                case TypeCode.Int32:
                                    if (int.TryParse(checkValue, out int resultInt32))
                                        value = Expression.Constant(resultInt32, valuetype);
                                    break;
                                case TypeCode.UInt32:
                                    if (uint.TryParse(checkValue, out uint resultUInt32))
                                        value = Expression.Constant(resultUInt32, valuetype);
                                    break;
                                case TypeCode.Int64:
                                    if (long.TryParse(checkValue, out long resultInt64))
                                        value = Expression.Constant(resultInt64, valuetype);
                                    break;
                                case TypeCode.UInt64:
                                    if (ulong.TryParse(checkValue, out ulong resultUInt64))
                                        value = Expression.Constant(resultUInt64, valuetype);
                                    break;
                                case TypeCode.Single:
                                    if (float.TryParse(checkValue, out float resultSingle))
                                        value = Expression.Constant(resultSingle, valuetype);
                                    break;
                                case TypeCode.Double:
                                    if (double.TryParse(checkValue, out double resultDouble))
                                        value = Expression.Constant(resultDouble, valuetype);
                                    break;
                                case TypeCode.Decimal:
                                    if (decimal.TryParse(checkValue, out decimal resultDecimal))
                                        value = Expression.Constant(resultDecimal, valuetype);
                                    break;
                                case TypeCode.String:
                                case TypeCode.Boolean:
                                case TypeCode.DateTime:
                                    value = Expression.Constant(checkValue, typeof(object));
                                    break;
                                case TypeCode.Empty:
                                case TypeCode.Object:
                                case TypeCode.DBNull:
                                default:
                                    value = Expression.Constant(Convert.ChangeType(checkValue, propertyExp.Type), propertyExp.Type);
                                    break;
                            }

                            if (value != null)
                            {
                                Expression hasValueExp = null;
                                if (isNullableValue)
                                {
                                    var propHasValue = Expression.Property(propertyExp, "HasValue");
                                    hasValueExp = Expression.Equal(propHasValue, Expression.Constant(true));
                                }

                                var toStringMethod = typeof(object).GetMethod("ToString", Type.EmptyTypes);
                                var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                                var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                                var enumableContainsMethod = typeof(Enumerable).GetMethods().FirstOrDefault(c => c.Name == "Contains" && c.GetParameters().Length == 2).MakeGenericMethod(typeof(string));

                                var valueToStringExp = Expression.Call(value, toStringMethod);
                                var valueToLowerExp = Expression.Call(valueToStringExp, toLowerMethod);

                                var toStringExp = Expression.Call(propertyExp, toStringMethod);
                                if (typecode == TypeCode.DateTime)
                                {
                                    var propValue = propertyExp;
                                    if (isNullableValue)
                                    {
                                        propValue = Expression.Property(propertyExp, "Value");
                                    }
                                    toStringExp = Expression.Call(propValue, toStringMethod.Name, null, new Expression[] { Expression.Constant("yyyy-MM-dd") });
                                }
                                var toLowerExp = Expression.Call(toStringExp, toLowerMethod);

                                Expression containsMethodExp;
                                if (isEnumableValue)
                                    containsMethodExp = Expression.Call(enumableContainsMethod, propertyExp, valueToStringExp);
                                else
                                    containsMethodExp = Expression.Call(toLowerExp, containsMethod, valueToLowerExp);

                                if (isNullableValue)
                                    exp = Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(hasValueExp, containsMethodExp), parameterExp);
                                else
                                    exp = Expression.Lambda<Func<TEntity, bool>>(containsMethodExp, parameterExp);
                            }
                        }
                    }
                }

                if (filter.AdditionalFilterColumns.Any())
                {
                    var childExp = ComposeFiltering<TEntity>(filter.AdditionalFilterColumns);
                    exp = childExp;
                }

                if (current != null)
                {
                    if (exp != null)
                    {
                        switch (filter.LogicalOperator)
                        {
                            case LogicalOperatorEnum.And:
                                return current.And(exp);
                            case LogicalOperatorEnum.Or:
                                return current.Or(exp);
                        }
                    }

                    return current;
                }

                return exp;
            });
        }
    }
}
