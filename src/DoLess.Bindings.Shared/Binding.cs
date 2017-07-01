using System;
using System.Collections.Generic;
using System.Text;

namespace DoLess.Bindings
{
    internal abstract class Binding :
        IInternalBinding
    {
        private static IdentifierPool IdPool = new IdentifierPool();

        public Binding(BindingGroup bindingGroup)
        {
            this.BindingGroup = bindingGroup ?? new BindingGroup();            
            this.Id = IdPool.Next();
            Bindings.Add(this.BindingGroup);
            this.BindingGroup.Add(this);
        }

        public BindingGroup BindingGroup { get; }

        public long Id { get; }

        public abstract bool CanBePurged();

        public virtual  void Dispose()
        {
            IdPool.Recycle(this.Id);
        }

        public void DeleteFromGroup()
        {
            this.BindingGroup?.Unbind(this);
        }

        public void Unbind()
        {
            this.BindingGroup.Unbind();
        }
    }
}
