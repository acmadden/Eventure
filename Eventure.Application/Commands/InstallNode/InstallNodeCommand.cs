using System;
using MediatR;

namespace Eventure.Application.Commands
{
    public class InstallNodeCommand : IRequest
    {
        public Guid StoreId { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string SystemType { get; set; }
    }
}