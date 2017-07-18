using System.Collections.Generic;
using Newtonsoft.Json;

namespace LuckyCode.WebFrameWork.Rendering {
    /// <summary>
    /// 表示一个元素构建器选项
    /// </summary>
    public class ElementBuilderOptions {
        [JsonIgnore]
        public Dictionary<string, object> Attributes {
            get; set;
        }
    }
}