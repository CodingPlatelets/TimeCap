﻿namespace TimCap.Model
{
    public class ApiResponse
    {
        public ApiCode Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public ApiResponse() { }
        public ApiResponse(ApiCode code, string message, object data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }

    public enum ApiCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 失败
        /// </summary>
        Error = -1
    }
}