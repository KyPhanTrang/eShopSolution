using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public string[] ValidationErrors { get; set; }

        public ApiErrorResult()
        {
            IsSuccess = false;
        }

        public ApiErrorResult(string message)
        {
            IsSuccess = false;
            Message = message;
        }

        public ApiErrorResult(string[] validationErrors)
        {
            IsSuccess = false;
            ValidationErrors = validationErrors;
        }
    }
}