using HR.LeaveManagement.Application.DTOs.LeaveAllLocation;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Queries
{
    public class GetLeaveAllLocationListRequest : IRequest<List<LeaveAllLocationDto>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}