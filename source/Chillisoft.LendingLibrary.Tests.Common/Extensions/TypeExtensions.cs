using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using Chillisoft.LendingLibrary.Tests.Common.Helpers;
using NUnit.Framework;
using PeanutButter.TestUtils.Generic;

namespace Chillisoft.LendingLibrary.Tests.Common.Extensions
{
    public static class TypeExtensions
    {
        public static void ShouldHaveAttribute<TAttribute>(this Type type, string onProperty) where TAttribute : Attribute
        {
            var propInfo = type.GetProperty(onProperty);
            Assert.IsNotNull(propInfo, string.Concat(
                "Could not find property '",
                onProperty,
                "' on type '",
                type.PrettyName(),
                "'"
            ));

            var attrib = ReflectionHelpers.GetAttribute<TAttribute>(propInfo);
            Assert.IsNotNull(attrib, string.Concat(
                "Could not find attribute of type '",
                typeof(TAttribute).PrettyName(),
                "' on property '", onProperty,
                "' on type '", type.PrettyName(), "'"));
        }

        [DebuggerStepThrough]
        public static void ShouldHaveAttribute<TAttribute>(this object objectUnderTest, Expression<Action> onMethod)
            where TAttribute : Attribute
        {
            var methodInvocation = onMethod.Body as MethodCallExpression;
            Assert.IsNotNull(methodInvocation, "Could not create method invocation.");
            var methodInfo = methodInvocation.Method;
            var sourceType = objectUnderTest.GetType();
            var methodName = methodInfo.Name;

            Assert.IsNotNull(methodInfo, string.Concat(
                "Could not find method '",
                methodName,
                "' on type '",
                sourceType.PrettyName(),
                "'"
            ));

            var attrib = methodInfo.GetCustomAttribute<TAttribute>();

            Assert.IsNotNull(attrib, string.Concat(
                "Could not find attribute of type '",
                typeof(TAttribute).PrettyName(),
                "' on method '", methodName,
                "' on type '", sourceType.PrettyName(), "'"));
        }

    }
}
