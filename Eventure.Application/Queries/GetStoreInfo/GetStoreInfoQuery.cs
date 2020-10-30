using System;
using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Queries
{
    public class GetStoreInfoQuery : IRequest<Store>
    {
        public Guid Id { get; set; }
    }
}