using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllLocation.Validators
{
    public class CreateLeaveAllLocationDtoValidator : AbstractValidator<CreateLeaveAllLocationDto>
    {
        public CreateLeaveAllLocationDtoValidator()
        {
            RuleFor(key => key.NumbersOfDays)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} must be at least 1.")
                .LessThan(60).WithMessage("{PropertyName} must be less than {ComprasionValue}.");

            RuleFor(key => key.LeaveTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(key => key.Period)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0).WithMessage("{PropertyName} must be at least 1.")
                .LessThan(365).WithMessage("{PropertyName} must be less than {ComprasionValue}.");
        }
    }
}