using System;

namespace Eventure.Application.ReadStore.ReadModels
{
    public class NodeReadModel
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string SystemType { get; set; }
        public bool Offline { get; set; }
        public string OfflineReason { get; set; }
    }
}