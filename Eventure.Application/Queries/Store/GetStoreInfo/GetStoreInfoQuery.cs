using System;
using Eventure.Application.ReadStore.ReadModels;
using MediatR;

namespace Eventure.Application.Queries
{
    public class GetStoreInfoQuery : IRequest<StoreReadModel>
    {
        public Guid Id { get; set; }
    }
}