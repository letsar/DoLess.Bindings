using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    /// <summary>
    /// Represents a binding.
    /// </summary>
    public interface IBinding : IDisposable
    {
        BindingParameters Parameters { get; }
    }
}
