using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using Xunit;

namespace HR.LeaveManagement.Application.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly CreateLeaveTypeDto _createLeaveTypeDto;
        private readonly CreateLeaveTypeCommandHandler _createLeaveTypeCommandHandler;

        public CreateLeaveTypeCommandHandlerTests()
        {
            _unitOfWork = MockUnitOfWork.GetUnitOfWorkMock();

            var mapperConfig = new MapperConfiguration(key =>
            {
                key.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _createLeaveTypeCommandHandler = new CreateLeaveTypeCommandHandler(_mapper, _unitOfWork.Object);

            _createLeaveTypeDto = new CreateLeaveTypeDto
            {
                DefaultDays = 15,
                Name = "Name 123",
            };
        }

        [Fact]
        public async Task CreateLeaveTypeCommandHandler_Success()
        {
            var result = await _createLeaveTypeCommandHandler.Handle(new CreateLeaveTypeCommand { LeaveTypeDto = _createLeaveTypeDto }, CancellationToken.None);
            var leaveTypes = await _unitOfWork.Object.LeaveTypeRepository.GetAllAsync();

            result.ShouldBeOfType<BaseCommandResponse>();
            leaveTypes.Count.ShouldBe(4);
        }

        [Fact]
        public async Task CreateLeaveTypeCommandHandler_InValid_Failed()
        {
            _createLeaveTypeDto.DefaultDays = -1;
            var result = await _createLeaveTypeCommandHandler.Handle(new CreateLeaveTypeCommand { LeaveTypeDto = _createLeaveTypeDto }, CancellationToken.None);
            var leaveTypes = await _unitOfWork.Object.LeaveTypeRepository.GetAllAsync();

            leaveTypes.Count.ShouldBe(3);
            result.ShouldBeOfType<BaseCommandResponse>();
        }
    }
}