using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace KnightFrank.DAL.Extensions
{
    public static class EnumExtension
    {
        public static string ToDescription(this Enum source)
        {
            Type genericEnumType = source.GetType();
            MemberInfo[] memberInfos = genericEnumType.GetMember(source.ToString());
            if (memberInfos != null && memberInfos.Length > 0)
            {
                var attributes = memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Count() > 0)
                {
                    return ((DescriptionAttribute)attributes.ElementAt(0)).Description;
                }
            }
            return source.ToString();
        }
    }
}
