using System;
using System.Linq;
using System.Reflection;

namespace Chillisoft.LendingLibrary.Tests.Common.Helpers
{
    public class ReflectionHelpers
    {
        public static T GetAttribute<T>(PropertyInfo prop) where T : Attribute
        {
            var attrs = prop.GetCustomAttributes(true);
            var attr = attrs.FirstOrDefault(o => o is T);
            return (T)attr;
        }

        public static T GetAttribute<T>(MethodInfo methodInfo) where T : Attribute
        {
            var attrs = methodInfo.GetCustomAttributes(true);
            var attr = attrs.FirstOrDefault(o => o is T);
            return (T)attr;
        }

        public static T GetAttribute<T>(Type type) where T : Attribute
        {
            return type.GetCustomAttributes(typeof(T), true
            ).FirstOrDefault() as T;

        }

    }
}