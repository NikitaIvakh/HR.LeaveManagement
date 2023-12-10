using AutoMapper;
using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Models.LeaveTypes;
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

        public async Task<List<LeaveTypeViewModel>> GetLeaveTypesAsync()
        {
            AddBearerToken();
            var leaveTypes = await _client.LeaveTypesAllAsync();
            return _mapper.Map<List<LeaveTypeViewModel>>(leaveTypes);
        }

        public async Task<LeaveTypeViewModel> GetLeaveTypeAsync(int id)
        {
            AddBearerToken();
            LeaveTypeDto leaveType = await _client.LeaveTypesGETAsync(id);
            return _mapper.Map<LeaveTypeViewModel>(leaveType);
        }

        public async Task<BaseResponse<int>> CreateLeaveTypeAsync(CreateLeaveTypeViewModel leaveType)
        {
            try
            {
                BaseResponse<int> response = new();
                CreateLeaveTypeDto createLeaveTypeDto = _mapper.Map<CreateLeaveTypeDto>(leaveType);
                AddBearerToken();
                BaseCommandResponse apiResponse = await _client.LeaveTypesPOSTAsync(createLeaveTypeDto);

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

        public async Task<BaseResponse<int>> UpdateLeaveTypeAsync(int id, UpdateLeaveTypeViewModel updateLeaveTypeViewModel)
        {
            try
            {
                BaseResponse<int> response = new();
                UpdateLeaveTypesDto leaveTypeDto = _mapper.Map<UpdateLeaveTypesDto>(updateLeaveTypeViewModel);
                AddBearerToken();
                BaseCommandResponse apiResponse = await _client.LeaveTypesPUTAsync(id.ToString(), leaveTypeDto);

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

        public async Task<BaseResponse<int>> DeleteLeaveTypeAsync(int id, DeleteLeaveTypeViewModel deleteLeaveTypeViewModel)
        {
            try
            {
                BaseResponse<int> response = new();
                DeleteLeaveTypeDto deleteLeaveTypeDto = _mapper.Map<DeleteLeaveTypeDto>(deleteLeaveTypeViewModel);
                AddBearerToken();
                BaseCommandResponse apiResponse = await _client.LeaveTypesDELETEAsync(id.ToString(), deleteLeaveTypeDto);

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
    }
}