using AutoMapper;
using HR.LeaveManagement.Application.DTOs.LeaveAllLocation.Validators;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllLocations.Requests.Commands;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllLocations.Handlers.Commands
{
    public class UpdateLeaveAllLocationsCommandHandler : IRequestHandler<UpdateLeaveAllLocationsCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveAllLocationsCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateLeaveAllLocationsCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateLeaveAllLocationDtoValidator(_unitOfWork.LeaveTypeRepository);
            var validatorResult = await validator.ValidateAsync(request.LeaveAllLocationDto, cancellationToken);

            if (validatorResult.IsValid is false)
                throw new ValidationException(validatorResult);

            var leaveAllLocation = await _unitOfWork.LeaveAllLocationRepository.GetAsync(request.LeaveAllLocationDto.Id)
                ?? throw new NotFoundException(nameof(LeaveAllLocation), request.LeaveAllLocationDto.Id);

            _mapper.Map(request.LeaveAllLocationDto, leaveAllLocation);
            await _unitOfWork.LeaveAllLocationRepository.UpdateAsync(leaveAllLocation);
            await _unitOfWork.Save();

            return Unit.Value;
        }
    }
}