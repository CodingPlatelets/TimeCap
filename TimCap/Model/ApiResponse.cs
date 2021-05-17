namespace TimCap.Model
{
    public class ApiResponse
    {
        /// <summary>
        /// 返回值
        /// </summary>
        public ApiCode Code { get; set; }
        /// <summary>
        /// 说明消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
        public ApiResponse() { }
        public ApiResponse(ApiCode code, string message, object data)
        {
            Code = code;
            Message = message;
            Data = data;
        }
    }

    /// <summary>
    /// 返回值 0成功 -1错误
    /// </summary>
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
