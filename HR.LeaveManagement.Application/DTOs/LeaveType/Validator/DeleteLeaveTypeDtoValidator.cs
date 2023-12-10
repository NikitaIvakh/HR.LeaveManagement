using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validator
{
    public class DeleteLeaveTypeDtoValidator : AbstractValidator<DeleteLeaveTypeDto>
    {
        public DeleteLeaveTypeDtoValidator()
        {
            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} must be present");
        }
    }
}