using HR.LeaveManagement.Application.DTOs.LeaveAllLocation;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Queries;
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
        [HttpGet("GetLeaveAllLocations")]
        public async Task<ActionResult<List<LeaveAllLocationDto>>> GetLeaveAllLocations()
        {
            var leaveAllLocations = await _mediator.Send(new GetLeaveAllLocationListRequest());
            return Ok(leaveAllLocations);
        }

        // GET api/<LeaveAllLocationsController>/5
        [HttpGet("GetLeaveAllLocation/{id}")]
        public async Task<ActionResult<LeaveAllLocationDto>> GetLeaveAllLocation(int id)
        {
            var leaveAllLocation = await _mediator.Send(new GetLeaveAllLocationDetailsRequest());
            return Ok(leaveAllLocation);
        }

        // POST api/<LeaveAllLocationsController>
        [HttpPost("CreateLeaveAllLocation")]
        public async Task<ActionResult<LeaveAllLocationDto>> CreateLeaveAllLocation([FromBody] CreateLeaveAllLocationDto leaveAllLocationDto)
        {
            var command = new CreateLeaveAllLocationsCommand() { LeaveAllLocationDto = leaveAllLocationDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<LeaveAllLocationsController>/5
        [HttpPut("UpdateLeaveAllLocation/{id}")]
        public async Task<ActionResult> UpdateLeaveAllLocation([FromBody] UpdateLeaveAllLocationDto leaveAllLocationDto)
        {
            var command = new UpdateLeaveAllLocationsCommand() { LeaveAllLocationDto = leaveAllLocationDto };
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<LeaveAllLocationsController>/5
        [HttpDelete("DeleteLeaveAllLocation/{id}")]
        public async Task<ActionResult> DeleteLeaveAllLocation(int id)
        {
            var command = new DeleteLeaveAllLocationsCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}