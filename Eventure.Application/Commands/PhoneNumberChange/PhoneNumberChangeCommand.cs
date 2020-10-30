using System;
using MediatR;

namespace Eventure.Application.Commands
{
    public class PhoneNumberChangeCommand : IRequest
    {
        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
    }
}