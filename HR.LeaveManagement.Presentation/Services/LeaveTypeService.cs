using AutoMapper;
using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Models;
using HR.LeaveManagement.Presentation.Services.Base;

namespace HR.LeaveManagement.Presentation.Services
{
    public class LeaveTypeService : BaseHttpService, ILeaveTypeService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IMapper _mapper;
        private readonly IClient _httpClient;

        public LeaveTypeService(IMapper mapper, ILocalStorageService localStorageService, IClient httpClient) : base(localStorageService, httpClient)
        {
            _localStorageService = localStorageService;
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public async Task<List<LeaveTypeViewModel>> GetLeaveTypes()
        {
            ICollection<LeaveTypeDto> leaveTypes = await _client.GetAllLeaveTypesAsync();
            return _mapper.Map<List<LeaveTypeViewModel>>(leaveTypes);
        }

        public async Task<LeaveTypeViewModel> GetLeaveType(int id)
        {
            LeaveTypeDto leaveType = await _client.GetLeaveTypeAsync(id);
            return _mapper.Map<LeaveTypeViewModel>(leaveType);
        }

        public async Task<BaseResponse<int>> CreateLeaveType(CreateLeaveTypeViewModel leaveType)
        {
            try
            {
                BaseResponse<int> response = new();
                CreateLeaveTypeDto createLeaveTypeDto = _mapper.Map<CreateLeaveTypeDto>(leaveType);
                BaseCommandResponse apiResponse = await _client.CreateLeaveTypeAsync(createLeaveTypeDto);

                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Status = true;
                }

                else
                {
                    foreach (string error in apiResponse.Errors)
                    {
                        response.ValidationErrors += error + Environment.NewLine;
                    }
                }

                return response;
            }

            catch (ApiException apiException)
            {
                return ConvertApiExceptions<int>(apiException);
            }
        }

        public async Task<BaseResponse<int>> UpdateLeaveType(int id, LeaveTypeViewModel leaveType)
        {
            try
            {
                LeaveTypeDto leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveType);
                await _client.UpdateLeaveTypeAsync(id.ToString(), leaveTypeDto);
                return new BaseResponse<int> { Status = true };
            }

            catch (ApiException apiException)
            {
                return ConvertApiExceptions<int>(apiException);
            }
        }

        public async Task<BaseResponse<int>> DeleteLeaveType(int id)
        {
            try
            {
                await _client.DeleteLeaveTypeAsync(id);
                return new BaseResponse<int> { Status = true };
            }

            catch (ApiException apiException)
            {
                return ConvertApiExceptions<int>(apiException);
            }
        }
    }
}