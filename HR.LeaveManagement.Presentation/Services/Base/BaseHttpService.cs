using HR.LeaveManagement.Presentation.Contracts;
using System.Net.Http.Headers;

namespace HR.LeaveManagement.Presentation.Services.Base
{
    public class BaseHttpService
    {
        protected readonly ILocalStorageService _localStorage;
        protected readonly IClient _client;

        public BaseHttpService(ILocalStorageService localStorage, IClient client)
        {
            _localStorage = localStorage;
            _client = client;
        }

        protected BaseResponse<Guid> ConvertApiExceptions<Guid>(ApiException exception)
        {
            if (exception.StatusCode == 400)
                return new BaseResponse<Guid>() { Message = "Validation errors have occured.", ValidationErrors = exception.Response, Status = false };

            else if (exception.StatusCode == 404)
                return new BaseResponse<Guid>() { Message = "The required item could not be found.", Status = false };

            else
                return new BaseResponse<Guid>() { Message = "Somethins went wrong, pleace try again.", Status = false };
        }

        protected void AddBearerToken()
        {
            if (_localStorage.Exists("token"))
                _client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _localStorage.GetStorageValue<string>("token"));
        }
    }
}