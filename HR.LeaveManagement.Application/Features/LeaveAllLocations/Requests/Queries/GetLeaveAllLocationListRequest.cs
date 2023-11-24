using HR.LeaveManagement.Application.DTOs;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Queries
{
    public class GetLeaveAllLocationListRequest : IRequest<List<LeaveAllLocationDto>>
    {

    }
}