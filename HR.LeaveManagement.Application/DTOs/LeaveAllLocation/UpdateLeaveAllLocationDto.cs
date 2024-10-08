﻿using HR.LeaveManagement.Application.DTOs.Common;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllLocation
{
    public class UpdateLeaveAllLocationDto : BaseDto, ILeaveAllLocationDto
    {
        public int NumbersOfDays { get; set; }

        public int LeaveTypeId { get; set; }

        public int Period { get; set; }
    }
}