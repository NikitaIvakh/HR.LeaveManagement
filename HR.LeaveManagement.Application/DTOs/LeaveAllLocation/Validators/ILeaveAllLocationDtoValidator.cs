using FluentValidation;
using HR.LeaveManagement.Application.Persistence.Contracts;

namespace HR.LeaveManagement.Application.DTOs.LeaveAllLocation.Validators
{
    public class ILeaveAllLocationDtoValidator : AbstractValidator<ILeaveAllLocationDto>
    {
        private readonly ILeaveAllLocationRepository _leaveAllLocationRepository;

        public ILeaveAllLocationDtoValidator(ILeaveAllLocationRepository leaveAllLocationRepository)
        {
            _leaveAllLocationRepository = leaveAllLocationRepository;

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

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await _leaveAllLocationRepository.Exists(id);
                    return !leaveTypeExists;
                })
                .WithMessage("{PropertyName} does not exist.");
        }
    }
}