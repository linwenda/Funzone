using System.Threading.Tasks;
using Funzone.Application.Zones.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Funzone.Api.Controllers.Zones
{
    [Route("api/zones")]
    [ApiController]
    public class ZonesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ZonesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateZone(CreateZoneCommand command)
        {
            var zoneId = await _mediator.Send(command);
            return Created("", zoneId);
        }
    }
}