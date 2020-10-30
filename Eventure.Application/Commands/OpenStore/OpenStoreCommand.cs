using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Commands
{
    public class OpenStoreCommand : IRequest<Store>
    {
        public string Name { get; set; }
    }
}