using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveTypeController>
        [HttpGet("GetAllLeaveTypes")]
        public async Task<ActionResult<List<LeaveTypeDto>>> GetAllLeaveTypes()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeListRequest());
            return Ok(leaveTypes);
        }

        // GET api/<LeaveTypeController>/5
        [HttpGet("GetLeaveType/{id}")]
        public async Task<ActionResult<LeaveTypeDto>> GetLeaveType(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeDetailsRequest() { Id = id });
            return Ok(leaveType);
        }

        // POST api/<LeaveTypeController>
        [HttpPost("CreateLeaveType")]
        public async Task<ActionResult> CreateLeaveType([FromBody] CreateLeaveTypeDto leaveTypeDto)
        {
            var command = new CreateLeaveTypeCommand() { LeaveTypeDto = leaveTypeDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<LeaveTypeController>
        [HttpPut("UpdateLeaveType")]
        public async Task<ActionResult> UpdateLeaveType([FromBody] LeaveTypeDto leaveTypeDto)
        {
            var command = new UpdateLeaveTypeCommand() { LeaveTypeDto = leaveTypeDto };
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<LeaveTypeController>/5
        [HttpDelete("DeleteLeaveType/{id}")]
        public async Task<ActionResult> DeleteLeaveType(int id)
        {
            var command = new DeleteLeaveTypeCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}