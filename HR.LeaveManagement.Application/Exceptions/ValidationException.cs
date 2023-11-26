using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(ValidationResult validationResult)
        {
            Errors = new List<string>();

            foreach (var error in validationResult.ErrorMessage)
            {
                Errors.Add(error.ToString());
            }
        }

        public List<string> Errors { get; set; }
    }
}