using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    internal abstract class Binding :
        IBinding,
        IHaveLinkedBinding
    {
        public Binding(IBinding linkedBinding, long id)
        {
            this.LinkedBinding = linkedBinding;
            this.Id = id;
            Bindings.Add(this);
        }

        public IBinding LinkedBinding { get; }        

        public long Id { get; set; }

        public void Unbind()
        {
            if (this.LinkedBinding != null)
            {
                this.LinkedBinding.Unbind();
            }
            else
            {
                // End of the chain.
                Bindings.Remove(this);
            }
            this.UnbindInternal();
        }

        public abstract void UnbindInternal();

        public abstract bool CanBePurged();
    }
}
