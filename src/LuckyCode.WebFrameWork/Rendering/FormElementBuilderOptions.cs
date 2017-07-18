using Newtonsoft.Json;

namespace LuckyCode.WebFrameWork.Rendering {
    /// <summary>
    /// 表示一个表单元素的属性信息
    /// </summary>
    public class FormElementBuilderOptions : ElementBuilderOptions {
        [JsonIgnore]
        public string Name {
            get; set;
        }

        [JsonIgnore]
        public object Value {
            get; set;
        }
    }
}