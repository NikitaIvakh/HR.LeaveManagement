using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.UnitTests.Common;
using Moq;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public static class MockUnitOfWork
    {
        public static Mock<IUnitOfWork> GetUnitOfWorkMock()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLeaveTypeRepository = MockLeaveTypeRepository.GetLeaveTypeRepository();

            mockUnitOfWork.Setup(key => key.LeaveTypeRepository).Returns(mockLeaveTypeRepository.Object);
            return mockUnitOfWork;
        }
    }
}