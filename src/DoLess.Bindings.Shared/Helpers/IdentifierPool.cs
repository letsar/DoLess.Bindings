using System.Collections.Generic;

namespace DoLess.Bindings
{
    internal class IdentifierPool
    {
        private const int DefaultLowestUnassignedId = 1;
        private readonly Queue<long> unusedIds;
        private long lowestUnassignedId;

        public IdentifierPool()
        {
            this.unusedIds = new Queue<long>();
            this.lowestUnassignedId = DefaultLowestUnassignedId;
        }

        public long Next()
        {
            lock (this.unusedIds)
            {
                if (this.unusedIds.Count > 0)
                {
                    return this.unusedIds.Dequeue();
                }
                else
                {
                    return this.lowestUnassignedId++;
                }
            }
        }

        public void Recycle(long id)
        {
            lock (this.unusedIds)
            {
                // The id cannot be 0 because the 0 value means no id.
                // The id cannot be greater than lowestUnassignedId, because it is not yet created.
                if (id > 0 && id < this.lowestUnassignedId)
                {
                    this.unusedIds.Enqueue(id);
                }
            }
        }
    }
}
