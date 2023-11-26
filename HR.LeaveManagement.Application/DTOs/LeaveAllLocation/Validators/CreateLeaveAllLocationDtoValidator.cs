using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllLocation.Validators
{
    public class CreateLeaveAllLocationDtoValidator : AbstractValidator<CreateLeaveAllLocationDto>
    {
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;

        public CreateLeaveAllLocationDtoValidator(ILeaveAllLocationRepository leaveAllLocationRepository)
        {
            _leaveAllLocationRepository = leaveAllLocationRepository;
            Include(new ILeaveAllLocationDtoValidator(_leaveAllLocationRepository));
        }
    }
}