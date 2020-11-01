using System;
using MediatR;

namespace Eventure.Application.Commands
{
    public class ChangeStorePhoneNumberCommand : IRequest
    {
        public Guid AggregateId { get; set; }
        public string PhoneNumber { get; set; }
    }
}