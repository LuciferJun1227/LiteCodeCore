namespace LuckyCode.Core.Utility
{
    public class ListItemEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 显示的内容
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 父编号
        /// </summary>
        public string ParentID { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool Selected { get; set; }
    }
}
