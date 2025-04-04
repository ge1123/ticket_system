using System;

namespace TicketSystem.Domain.Exceptions
{
    /// <summary>
    /// 業務邏輯異常
    /// </summary>
    public class BusinessException : Exception
    {
        /// <summary>
        /// 錯誤代碼
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// 初始化業務邏輯異常
        /// </summary>
        /// <param name="message">錯誤消息</param>
        public BusinessException(string message) : base(message)
        {
        }

        /// <summary>
        /// 初始化業務邏輯異常
        /// </summary>
        /// <param name="message">錯誤消息</param>
        /// <param name="errorCode">錯誤代碼</param>
        public BusinessException(string message, string errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// 初始化業務邏輯異常
        /// </summary>
        /// <param name="message">錯誤消息</param>
        /// <param name="innerException">內部異常</param>
        public BusinessException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>
        /// 初始化業務邏輯異常
        /// </summary>
        /// <param name="message">錯誤消息</param>
        /// <param name="errorCode">錯誤代碼</param>
        /// <param name="innerException">內部異常</param>
        public BusinessException(string message, string errorCode, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }
    }
} 