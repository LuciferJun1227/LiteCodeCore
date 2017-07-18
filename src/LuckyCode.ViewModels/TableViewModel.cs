using System.Collections.Generic;
using Newtonsoft.Json;

namespace LuckyCode.ViewModels
{
    public class TableViewModel<T>
    {
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }
        [JsonProperty(PropertyName = "rows")]
        public List<T> Rows { get; set; }
    }

    public class ReturnResult
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public string ErrorCode { get; set; }
        /// <summary>
        /// 相关信息
        /// </summary>
        public string Message { get; set; }
    }

    public class ResultItem<T> : ReturnResult
    {
        public T Data { get; set; }
    }
}
