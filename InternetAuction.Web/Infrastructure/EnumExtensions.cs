using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace InternetAuction.Web.Infrastructure
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            var firstMember = enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First();
            var attr = firstMember.GetCustomAttribute<DisplayAttribute>();

            if (attr is null)
            {
                return firstMember.Name;
            }

            return attr.GetName();
        }
    }
}