using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validator
{
    public class CreateLeaveTypeDtoValidator : AbstractValidator<CreateLeaveTypeDto>
    {
        public CreateLeaveTypeDtoValidator()
        {
            RuleFor(key => key.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().MaximumLength(50).WithMessage("{PropertyName} must not exceed {ComprasionValue} characters.");

            RuleFor(key => key.DefaultDays)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} must be at least 1.")
                .LessThan(100).WithMessage("{PropertyName} must be less than {ComprasionValue}.");
        }
    }
}