using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    internal abstract class Binding : IBinding, IHaveLinkedBinding
    {
        public Binding(IBinding linkedBinding)
        {
            this.LinkedBinding = linkedBinding;
        }

        public IBinding LinkedBinding { get; }

        public virtual void Unbind()
        {
            if (this.LinkedBinding != null)
            {
                this.LinkedBinding.Unbind();
            }
            this.UnbindInternal();
        }

        public abstract void UnbindInternal();
    }
}
