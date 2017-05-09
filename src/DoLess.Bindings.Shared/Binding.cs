using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    internal abstract class Binding :
        IBinding
    {
        private WeakReference<object> weakCreator;

        public Binding(IBinding linkedBinding, object creator)
        {
            this.LinkedBinding = linkedBinding;
            this.Id = linkedBinding == null ? 0 : linkedBinding.Id;
            this.weakCreator = new WeakReference<object>(creator);
            Bindings.Add(this);
        }

        public IBinding LinkedBinding { get; private set; }

        public long Id { get; set; }

        public object Creator => this.weakCreator.GetOrDefault();

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
            this.weakCreator = null;
        }

        public abstract bool CanBePurged();
    }
}
