﻿namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        ILeaveAllLocationRepository LeaveAllLocationRepository { get; }

        ILeaveRequestRepository LeaveRequestRepository { get; }

        ILeaveTypeRepository LeaveTypeRepository { get; }

        Task Save();
    }
}