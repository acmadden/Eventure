using Eventure.Domain.Entities;
using MediatR;

namespace Eventure.Application.Commands
{
    public class OpenStoreCommand : IRequest<Store>
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }
}