using AutoMapper;
using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Models.LeaveRequests;
using HR.LeaveManagement.Presentation.Services.Base;

namespace HR.LeaveManagement.Presentation.Services
{
    public class LeaveRequestService : BaseHttpService, ILeaveRequestService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IClient _httpClient;
        private readonly IMapper _mapper;

        public LeaveRequestService(ILocalStorageService localStorageService, IClient httpClient, IMapper mapper) : base(localStorageService, httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
            _mapper = mapper;
        }

        public async Task<BaseResponse<int>> CreateLeaveRequestAsync(CreateRequestViewModel createRequestViewModel)
        {
            try
            {
                BaseResponse<int> response = new();
                CreateLeaveRequestDto createLeaveRequestDto = _mapper.Map<CreateLeaveRequestDto>(createRequestViewModel);
                AddBearerToken();
                BaseCommandResponse apiResponse = await _client.LeaveRequestsPOSTAsync(createLeaveRequestDto);

                if (apiResponse.Success)
                {
                    response.Data = apiResponse.Id;
                    response.Status = true;
                }

                else
                {
                    foreach (var error in apiResponse.Errors)
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

        public async Task<BaseResponse<int>> DeleteLeaveRequest(int id)
        {
            try
            {
                await _client.LeaveRequestsDELETEAsync(id);
                AddBearerToken();
                return new BaseResponse<int> { Status = true };
            }

            catch (ApiException apiException)
            {
                return ConvertApiExceptions<int>(apiException);
            }
        }
    }
}