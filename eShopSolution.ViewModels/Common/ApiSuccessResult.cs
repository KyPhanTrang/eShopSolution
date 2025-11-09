using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult()
        {
            IsSuccess = true;
        }

        public ApiSuccessResult(T resultObj)
        {
            IsSuccess = true;
            ResultObj = resultObj;
        }
    }
}