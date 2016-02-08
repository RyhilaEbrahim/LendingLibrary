using System;
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
}
}
