using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Services.Base;

namespace HR.LeaveManagement.Presentation.Services
{
    public class LeaveAllLocationService : BaseHttpService, ILeaveAllLocationService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IClient _httpClient;

        public LeaveAllLocationService(ILocalStorageService localStorageService, IClient httpClient) : base(localStorageService, httpClient)
        {
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public async Task<BaseResponse<int>> CreateLeaveAllLocationAsync(int leaveTypeId)
        {
            try
            {
                BaseResponse<int> response = new();
                CreateLeaveAllLocationDto createLeaveAllLocationDto = new() { LeaveTypeId = leaveTypeId };
                AddBearerToken();
                BaseCommandResponse apiResponse = await _client.LeaveAllLocationsPOSTAsync(createLeaveAllLocationDto);

                if (apiResponse.Success)
                {
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