using System;
using System.Collections.Generic;
using System.Text;

namespace LuckyCode.WebFrameWork.TagHelper.MVCPager
{
    public class PagerMetaModel
    {
        public List<Page> Pages { get; set; }=new List<Page>();

        /// <summary>
        /// Previous Page node
        /// </summary>
        public PreviousPage PreviousPage { get; set; }

        /// <summary>
        /// Next page node
        /// </summary>
        public NextPage NextPage { get; set; }
    }
    /// <summary>
    /// 上一页
    /// </summary>
    public class PreviousPage
    {
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool Display { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public int PageNumber { get; set; }
    }
    /// <summary>
    /// 下一页
    /// </summary>
    public class NextPage
    {
        /// <summary>
        /// Do we need to display this node
        /// </summary>
        public bool Display { get; set; }

        /// <summary>
        /// Associated Page Number
        /// </summary>
        public int PageNumber { get; set; }
    }
    /// <summary>
    /// 分页显示的数字
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Associated Page Number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Is this the current page
        /// </summary>
        public bool IsCurrent { get; set; }
    }

}
