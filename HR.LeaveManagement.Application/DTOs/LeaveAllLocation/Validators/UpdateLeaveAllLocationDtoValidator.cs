using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllLocation.Validators
{
    public class UpdateLeaveAllLocationDtoValidator : AbstractValidator<UpdateLeaveAllLocationDto>
    {
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;

        public UpdateLeaveAllLocationDtoValidator(ILeaveAllLocationRepository leaveAllLocationRepository)
        {
            _leaveAllLocationRepository = leaveAllLocationRepository;
            Include(new ILeaveAllLocationDtoValidator(_leaveAllLocationRepository));

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}