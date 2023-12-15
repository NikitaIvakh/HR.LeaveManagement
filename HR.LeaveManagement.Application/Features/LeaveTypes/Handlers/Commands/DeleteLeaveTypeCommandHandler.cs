using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;
using HR.LeaveManagement.Application.Responses;
using HR.LeaveManagement.Application.DTOs.LeaveType.Validator;
using AutoMapper;

namespace HR.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteLeaveTypeCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseCommandResponse> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new DeleteLeaveTypeDtoValidator();
            var validationResult = await validator.ValidateAsync(request.DeleteLeaveTypeDto, cancellationToken);

            if (validationResult.IsValid is false)
            {
                response.Success = false;
                response.Message = "Delete Failed";
                response.Errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            }

            else
            {
                var leaveType = await _unitOfWork.LeaveTypeRepository.GetAsync(request.DeleteLeaveTypeDto.Id)
                    ?? throw new NotFoundException(nameof(LeaveType), request.DeleteLeaveTypeDto.Id);

                _mapper.Map(request.DeleteLeaveTypeDto, leaveType);
                await _unitOfWork.LeaveTypeRepository.DeleteAsync(leaveType);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Delete Successful";
                response.Id = leaveType.Id;
            }

            return response;
        }
    }
}