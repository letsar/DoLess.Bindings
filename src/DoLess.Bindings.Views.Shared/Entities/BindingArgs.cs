using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    internal partial class BindingArgs2 : IDisposable
    {
        partial void InternalDispose();

        public void Dispose()
        {
            this.InternalDispose();
        }
    }
}
