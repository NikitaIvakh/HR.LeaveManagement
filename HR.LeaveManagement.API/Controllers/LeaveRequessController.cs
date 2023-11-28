using HR.LeaveManagement.Application.DTOs.LeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequess.Requests.Commands;
using HR.LeaveManagement.Application.Features.LeaveRequess.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveRequessController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequessController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/<LeaveRequessController>
        [HttpGet("GetLeaveRequests")]
        public async Task<ActionResult<List<LeaveRequestDto>>> GetLeaveRequests()
        {
            var leaveRequests = await _mediator.Send(new GetLeaveRequestListRequest());
            return Ok(leaveRequests);
        }

        // GET api/<LeaveRequessController>/5
        [HttpGet("GetLeaveRequest/{id}")]
        public async Task<ActionResult<LeaveRequestDto>> GetLeaveRequest(int id)
        {
            var leaveRequest = await _mediator.Send(new GetLeaveRequestDetailsRequest() { Id = id });
            return Ok(leaveRequest);
        }

        // POST api/<LeaveRequessController>
        [HttpPost("CreateLeaveRequest")]
        public async Task<ActionResult<LeaveRequestDto>> CreateLeaveRequest([FromBody] CreateLeaveRequestDto leaveRequestDto)
        {
            var command = new CreateLeaveRequessCommand() { LeaveRequestDto = leaveRequestDto };
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<LeaveRequessController>/5
        [HttpPut("UpdateLeaveRequest/{id}")]
        public async Task<ActionResult> UpdateLeaveRequest(int id, [FromBody] UpdateLeaveRequestDto leaveRequestDto)
        {
            var command = new UpdateLeaveRequessCommand() { Id = id, LeaveRequest = leaveRequestDto };
            await _mediator.Send(command);
            return NoContent();
        }

        // PUT api/<LeaveAllLocationsController>/5
        [HttpPut("ChangeApproval/{id}")]
        public async Task<ActionResult> ChangeApproval(int id, [FromBody] ChangeLeaveRequestApplovalDto changeLeaveRequestApplovalDto)
        {
            var command = new UpdateLeaveRequessCommand() { Id = id, ChangeLeaveRequestApploval = changeLeaveRequestApplovalDto };
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<LeaveRequessController>/5
        [HttpDelete("DeleteLeaveRequest/{id}")]
        public async Task<ActionResult> DeleteLeaveRequest(int id)
        {
            var command = new DeleteLeaveRequessCommand() { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}