using Microsoft.AspNetCore.Builder;
using TicketSystem.Api.Middlewares;

namespace TicketSystem.Api.Extensions
{
    /// <summary>
    /// 異常處理中間件擴展方法
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        /// <summary>
        /// 使用全局異常處理中間件
        /// </summary>
        /// <param name="app">應用程序構建器</param>
        /// <returns>應用程序構建器</returns>
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
} 