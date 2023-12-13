using AutoMapper;
using HR.LeaveManagement.Presentation.Contracts;
using HR.LeaveManagement.Presentation.Models.LeaveAllLocations;
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

        public async Task<AdminLeaveRequestViewVM> GetAdminLeaveRequestViewViewModelAsync()
        {
            AddBearerToken();
            var leaveRequest = await _client.LeaveRequestsAllAsync(isLoggedInUser: false);

            var adminLeaveRequestViewVM = new AdminLeaveRequestViewVM
            {
                ApprovedRequests = leaveRequest.Count(key => key.Approved == true),
                PendingRequests = leaveRequest.Count(key => key.Approved == null),
                RejectedRequests = leaveRequest.Count(key => key.Approved == false),
                LeaveRequests = _mapper.Map<List<LeaveRequestViewModel>>(leaveRequest),
            };

            return adminLeaveRequestViewVM;
        }

        public async Task<EmployeeLeaveRequestViewVM> GetEmployeeLeaveRequestViewViewModelAsync()
        {
            AddBearerToken();
            var leaveRequest = await _client.LeaveRequestsAllAsync(isLoggedInUser: false);
            var leaveAllLocation = await _client.LeaveAllLocationsAllAsync(isLoggedInUser: false);

            var employeeLeaveRequestViewVM = new EmployeeLeaveRequestViewVM
            {
                LeaveRequests = _mapper.Map<List<LeaveRequestViewModel>>(leaveRequest),
                LeaveAllocations = _mapper.Map<List<LeaveAllocationViewModel>>(leaveAllLocation)
            };

            return employeeLeaveRequestViewVM;
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

        public async Task<LeaveRequestViewModel> GetLeaveRequestAsync(int id)
        {
            AddBearerToken();
            var leaveRequest = await _client.LeaveRequestsGETAsync(id);
            return _mapper.Map<LeaveRequestViewModel>(leaveRequest);
        }

        public async Task<BaseResponse<int>> DeleteLeaveRequestAsync(int id)
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

        public async Task ApproveLeaveRequestAsync(int id, bool approved)
        {
            AddBearerToken();
            try
            {
                var approveStatus = new ChangeLeaveRequestApplovalDto { Id = id, Approved = approved };
                await _client.ChangeapprovalAsync(id, approveStatus);
            }

            catch
            {
                throw;
            }
        }
    }
}