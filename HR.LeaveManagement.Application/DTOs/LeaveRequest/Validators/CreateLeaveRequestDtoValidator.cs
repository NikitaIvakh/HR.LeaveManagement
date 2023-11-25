using FluentValidation;
using HR.LeaveManagement.Application.Persistence.Contracts;

namespace HR.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class CreateLeaveRequestDtoValidator : AbstractValidator<CreateLeaveRequestDto>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveRequestDtoValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

            RuleFor(key => key.StartDate)
                .LessThan(key => key.EndDate).WithMessage("{PropertyName} must be before {ComprasionValue}");

            RuleFor(key => key.EndDate)
                .GreaterThan(key => key.StartDate).WithMessage("{PropertyName} must be after {ComprasionValue}");

            RuleFor(key => key.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(async (id, token) =>
                {
                    var leaveTypeExists = await _leaveTypeRepository.Exists(id);
                    return !leaveTypeExists;
                })
                .WithMessage("{PropertyName} does not exist.");
        }
    }
}