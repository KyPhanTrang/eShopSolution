using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T ResultObj { get; set; }
        public ApiResult(bool isSuccess, string message, T resultObj)
        {
            IsSuccess = isSuccess;
            Message = message;
            ResultObj = resultObj;
        }
        public ApiResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public ApiResult(bool isSuccess, T resultObj) 
        {
            IsSuccess = isSuccess;
            ResultObj = resultObj;
        }
    }
}
