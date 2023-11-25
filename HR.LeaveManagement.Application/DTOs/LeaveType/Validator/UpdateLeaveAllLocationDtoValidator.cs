using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validator
{
    public class UpdateLeaveAllLocationDtoValidator : AbstractValidator<LeaveTypeDto>
    {
        public UpdateLeaveAllLocationDtoValidator()
        {
            Include(new ILeaveTypeDtoValidator());
            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}