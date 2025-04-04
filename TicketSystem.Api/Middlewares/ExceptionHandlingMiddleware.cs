using System.Net;
using System.Text.Json;
using TicketSystem.Domain.Exceptions;

namespace TicketSystem.Api.Middlewares
{
    /// <summary>
    /// 全局異常處理中間件
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        /// <summary>
        /// 初始化異常處理中間件
        /// </summary>
        /// <param name="next">下一個中間件</param>
        /// <param name="logger">日誌記錄器</param>
        /// <param name="env">環境變量</param>
        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger,
            IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        /// <summary>
        /// 處理 HTTP 請求
        /// </summary>
        /// <param name="context">HTTP 上下文</param>
        /// <returns>異步任務</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // 調用管道中的下一個中間件
                await _next(context);
            }
            catch (Exception ex)
            {
                // 記錄異常
                _logger.LogError(ex, "發生未處理的異常: {Message}", ex.Message);
                
                // 處理異常
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// 處理異常並返回適當的響應
        /// </summary>
        /// <param name="context">HTTP 上下文</param>
        /// <param name="exception">異常</param>
        /// <returns>異步任務</returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // 設置響應內容類型
            context.Response.ContentType = "application/json";
            
            // 根據異常類型設置狀態碼
            var response = new ApiResponse<object>();
            
            switch (exception)
            {
                case BusinessException businessEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = businessEx.Message;
                    response.ErrorCode = businessEx.ErrorCode;
                    break;
                    
                case ArgumentException argEx:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.Message = argEx.Message;
                    break;
                    
                case KeyNotFoundException keyEx:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response.Message = keyEx.Message;
                    break;
                    
                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response.Message = "未授權的訪問";
                    break;
                    
                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.Message = "發生內部服務器錯誤";
                    break;
            }
            
            // 在開發環境中提供詳細的錯誤信息
            if (_env.IsDevelopment())
            {
                response.DeveloperMessage = new DeveloperErrorDetails
                {
                    Message = exception.Message,
                    StackTrace = exception.StackTrace
                };
            }
            
            // 序列化響應並寫入響應流
            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }
    }

    /// <summary>
    /// API 響應模型
    /// </summary>
    /// <typeparam name="T">響應數據類型</typeparam>
    public class ApiResponse<T>
    {
        /// <summary>
        /// 響應狀態
        /// </summary>
        public bool Success { get; set; } = false;
        
        /// <summary>
        /// 響應消息
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// 錯誤代碼
        /// </summary>
        public string ErrorCode { get; set; }
        
        /// <summary>
        /// 響應數據
        /// </summary>
        public T Data { get; set; }
        
        /// <summary>
        /// 開發者錯誤詳情（僅在開發環境中提供）
        /// </summary>
        public DeveloperErrorDetails DeveloperMessage { get; set; }
    }

    /// <summary>
    /// 開發者錯誤詳情
    /// </summary>
    public class DeveloperErrorDetails
    {
        /// <summary>
        /// 錯誤消息
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// 堆棧跟踪
        /// </summary>
        public string StackTrace { get; set; }
    }
} 