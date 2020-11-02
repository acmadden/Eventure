using System;
using System.Threading.Tasks;
using Eventure.Application.Commands;
using Eventure.Application.Queries;
using Eventure.Application.Projection.ReadModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eventure.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<StoreReadModel> FetchStoreAsync(Guid id)
        {
            return await _mediator.Send(new GetStoreInfoQuery() { Id = id });
        }

        [HttpPost]
        public async Task<StoreReadModel> RegisterStoreAsync([FromBody] OpenStoreCommand command)
        {
            var store = await _mediator.Send(command);
            return new StoreReadModel()
            {
                Id = store.Id,
                Name = store.Name,
                PhoneNumber = store.PhoneNumber,
                Street = store.Location.Street,
                City = store.Location.City,
                State = store.Location.State,
                PostalCode = store.Location.PostalCode,
                Country = store.Location.Country
            };
        }

        [HttpPut("{id}/phone-number")]
        public async Task ChangeStorePhoneNumberAsync(Guid id, [FromBody] ChangeStorePhoneNumberCommand command)
        {
            command.AggregateId = id;
            await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task CloseStoreAsync(Guid id)
        {
            await _mediator.Send(new CloseStoreCommand() { Id = id });
        }
    }
}