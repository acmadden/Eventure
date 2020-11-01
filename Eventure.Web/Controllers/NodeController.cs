using System;
using System.Threading.Tasks;
using Eventure.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Eventure.Web.Controllers
{
    [Route("api/")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NodeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("store/{storeId}/node")]
        public async Task InstallNodeAsync(Guid storeId, [FromBody] InstallNodeCommand command)
        {
            command.StoreId = storeId;
            await _mediator.Send(command);
        }
    }
}