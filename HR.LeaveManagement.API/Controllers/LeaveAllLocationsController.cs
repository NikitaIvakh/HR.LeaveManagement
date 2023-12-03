using HR.LeaveManagement.Application.DTOs.LeaveAllLocation;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Queries;
using HR.LeaveManagement.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveAllLocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllLocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveAllLocationsController>
        [HttpGet]
        public async Task<ActionResult<List<LeaveAllLocationDto>>> Get()
        {
            List<LeaveAllLocationDto> leaveAllLocations = await _mediator.Send(new GetLeaveAllLocationListRequest());
            return Ok(leaveAllLocations);
        }

        // GET api/<LeaveAllLocationsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LeaveAllLocationDto>> Get(int id)
        {
            LeaveAllLocationDto leaveAllLocation = await _mediator.Send(new GetLeaveAllLocationDetailsRequest() { Id = id });
            return Ok(leaveAllLocation);
        }

        // POST api/<LeaveAllLocationsController>
        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveAllLocationDto leaveAllLocationDto)
        {
            CreateLeaveAllLocationsCommand command = new() { LeaveAllLocationDto = leaveAllLocationDto };
            BaseCommandResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<LeaveAllLocationsController>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllLocationDto leaveAllLocationDto)
        {
            var command = new UpdateLeaveAllLocationsCommand() { LeaveAllLocationDto = leaveAllLocationDto };
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<LeaveAllLocationsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteLeaveAllLocationsCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}