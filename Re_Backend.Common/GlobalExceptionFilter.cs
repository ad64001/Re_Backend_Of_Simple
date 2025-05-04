using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Re_Backend.Common.enumscommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Re_Backend.Common
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            // 定义默认的错误码和消息
            int code = (int)ResultEnum.unKnown;
            string message = "发生未知错误，请稍后重试。";

            // 这里可以根据不同的异常类型设置不同的错误码和消息
            // 例如：
            // if (context.Exception is CustomException customEx)
            // {
            //     code = customEx.Code;
            //     message = customEx.Message;
            // }

            // 构建响应对象
            var response = new
            {
                code,
                message
            };

            // 设置响应结果
            context.Result = new ObjectResult(response)
            {
                StatusCode = 500
            };

            // 标记异常已处理
            context.ExceptionHandled = true;
        }
    }
}
