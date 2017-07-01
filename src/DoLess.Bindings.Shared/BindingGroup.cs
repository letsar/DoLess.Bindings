using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoLess.Bindings
{
    internal class BindingGroup : IEnumerable<IInternalBinding>
    {
        private readonly Dictionary<long, IInternalBinding> bindings;

        public BindingGroup()
        {
            this.bindings = new Dictionary<long, IInternalBinding>();
        }

        public long Id { get; set; }

        public IEnumerator<IInternalBinding> GetEnumerator() => this.bindings.Values.GetEnumerator();

        public void Unbind()
        {
            foreach (var binding in this.bindings.Values.ToArray())
            {
                this.Unbind(binding);
            }
        }

        public void Unbind(IInternalBinding binding)
        {
            if (binding != null)
            {
                this.bindings.Remove(binding.Id);
                binding?.Dispose();
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public void Add(IInternalBinding binding)
        {
            if (binding != null)
            {
                this.bindings[binding.Id] = binding;
            }
        }
    }
}
