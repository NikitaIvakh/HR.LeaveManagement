using FluentValidation;

namespace HR.LeaveManagement.Application.DTOs.LeaveType.Validator
{
    public class CreateLeaveAllLocationDtoValidator : AbstractValidator<CreateLeaveTypeDto>
    {
        public CreateLeaveAllLocationDtoValidator()
        {
            Include(new ILeaveTypeDtoValidator());
        }
    }
}