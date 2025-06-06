﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Re_Backend.Common
{
    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;

        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 允许所有来源的跨域请求
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            // 允许携带凭证
            context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            // 允许的请求方法
            context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            // 允许的请求头
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization");
            // 预检请求的缓存时间（秒）
            context.Response.Headers.Add("Access-Control-Max-Age", "3600");

            // 处理预检请求
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = 204;
                // 移除写入响应体的代码
                return;
            }

            // 继续处理后续的中间件
            await _next(context);
        }
    }

    public static class CorsMiddlewareExtensions
    {
        public static IApplicationBuilder UseCorsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CorsMiddleware>();
        }
    }
}