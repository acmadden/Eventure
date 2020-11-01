using System.Collections.Generic;

namespace Eventure.Domain.Entities
{
    public class NodeStatus : ValueObject
    {
        public bool Offline { get; set; }
        public string Reason { get; set; }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Offline;
            yield return Reason;
        }
    }
}