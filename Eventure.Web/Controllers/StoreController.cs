using System;
using System.Threading.Tasks;
using Eventure.Application.Commands;
using Eventure.Application.Queries;
using Eventure.Web.ViewModels;
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
        public async Task<StoreViewModel> FetchStoreAsync(Guid id)
        {
            var store = await _mediator.Send(new GetStoreInfoQuery() { Id = id });
            return new StoreViewModel()
            {
                Id = store.Id,
                Name = store.Name,
                PhoneNumber = store.PhoneNumber
            };
        }

        [HttpPost]
        public async Task<StoreViewModel> RegisterStoreAsync([FromBody] OpenStoreCommand command)
        {
            var store = await _mediator.Send(command);
            return new StoreViewModel()
            {
                Id = store.Id,
                Name = store.Name,
                PhoneNumber = store.PhoneNumber
            };
        }
    }
}