using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T resultObj)
            : base(true, resultObj) { }
        public ApiSuccessResult(string message, T resultObj)
            :base(true, message, resultObj) { }
    }
}
