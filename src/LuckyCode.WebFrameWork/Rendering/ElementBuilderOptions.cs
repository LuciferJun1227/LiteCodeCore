using System.Collections.Generic;
using Newtonsoft.Json;

namespace LuckyCode.WebFrameWork.Rendering {
    /// <summary>
    /// ��ʾһ��Ԫ�ع�����ѡ��
    /// </summary>
    public class ElementBuilderOptions {
        [JsonIgnore]
        public Dictionary<string, object> Attributes {
            get; set;
        }
    }
}