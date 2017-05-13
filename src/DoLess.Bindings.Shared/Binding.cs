using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    internal abstract class Binding :
        IBinding
    {
        public Binding(IBinding linkedBinding)
        {
            this.LinkedBinding = linkedBinding;
            this.Id = linkedBinding == null ? 0 : linkedBinding.Id;            
            Bindings.Add(this);
        }

        public IBinding LinkedBinding { get; private set; }

        public long Id { get; set; }        

        public void Unbind()
        {
            if (this.LinkedBinding != null)
            {
                this.LinkedBinding.Unbind();
                this.LinkedBinding = null;
            }
            else
            {
                // End of the chain.
                Bindings.Remove(this);
            }
            this.UnbindInternal();
        }

        public virtual void UnbindInternal()
        {            
        }

        public abstract bool CanBePurged();
    }
}
