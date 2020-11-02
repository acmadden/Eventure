using System;
using MediatR;

namespace Eventure.Application.Commands
{
    public class CloseStoreCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}