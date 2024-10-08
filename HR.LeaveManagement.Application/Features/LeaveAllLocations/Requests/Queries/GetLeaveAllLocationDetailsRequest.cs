﻿using HR.LeaveManagement.Application.DTOs.LeaveAllLocation;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Queries
{
    public class GetLeaveAllLocationDetailsRequest : IRequest<LeaveAllLocationDto>
    {
        public int Id { get; set; }
    }
}