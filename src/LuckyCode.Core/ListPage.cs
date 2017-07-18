using System.Collections.Generic;

namespace LuckyCode.Core
{
    public class ListPage<T>:List<T>
    {
        public int Total { get; set; }

    }
}
