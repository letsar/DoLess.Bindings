﻿using System;

namespace DoLess.Bindings.Helpers
{
    internal static class Check
    {
        public static T NotNull<T>(T value, string paramName) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
            return value;
        }

        public static T? NotNull<T>(T? value, string paramName) where T : struct
        {
            if (!value.HasValue)
            {
                throw new ArgumentNullException(paramName);
            }
            return value;
        }
    }
}
