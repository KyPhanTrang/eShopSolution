using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.ViewModels.Common
{
    public class ApiErrorResult<T> : ApiResult<T>
    {
        public ApiErrorResult(T resultObj)
            : base(false, resultObj) { }
        public ApiErrorResult(string message, T resultObj)
            : base(false, message, resultObj) { }
    }
}
