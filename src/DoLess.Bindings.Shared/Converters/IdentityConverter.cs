using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings.Converters
{
    /// <summary>
    /// Represents the identy converter.
    /// This class is static for caching issues.
    /// </summary>
    internal static class IdentityConverter<T>
    {
        #region Public Methods
        /// <summary>
        /// The instance of this identity function.
        /// </summary>
        public static Func<T, T> Instance
        {
            get { return x => x; }
        }
        #endregion
    }
}
