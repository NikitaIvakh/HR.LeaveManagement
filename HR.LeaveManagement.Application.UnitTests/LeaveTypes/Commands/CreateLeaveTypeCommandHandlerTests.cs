using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.DTOs.LeaveType;
using HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Profiles;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.UnitTests.Common;
using Moq;
using Shouldly;
using Xunit;

namespace HR.LeaveManagement.Application.UnitTests.LeaveTypes.Commands
{
    public class CreateLeaveTypeCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeaveTypeRepository> _repository;
        private readonly CreateLeaveTypeDto _createLeaveTypeDto;
        private readonly CreateLeaveTypeCommandHandler _createLeaveTypeCommandHandler;

        public CreateLeaveTypeCommandHandlerTests()
        {
            _repository = MockLeaveTypeRepository.GetLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(key =>
            {
                key.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _createLeaveTypeCommandHandler = new CreateLeaveTypeCommandHandler(_repository.Object, _mapper);

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
            var leaveTypes = await _repository.Object.GetAllAsync();

            result.ShouldBeOfType<BaseCommandResponse>();
            leaveTypes.Count.ShouldBe(4);
        }
    }
}