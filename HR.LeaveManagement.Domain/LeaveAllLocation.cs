﻿using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain
{
    public class LeaveAllLocation : BaseDomainEntity
    {
        public int NumbersOfDays { get; set; }

        public LeaveType LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

        public int Period { get; set; }

        public string EmployeeId { get; set; }
    }
}