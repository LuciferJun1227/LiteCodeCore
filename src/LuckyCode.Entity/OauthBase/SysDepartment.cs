namespace LuckyCode.Entity.OauthBase
{
    public class SysDepartment
    {
        public SysDepartment()
        {

        }

        public string DepartmentId { get; set; }
        public int DistributorId { get; set; }
        public string ParentId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public int Sort { get; set; }

        //public virtual ICollection<SysDepartment> Departments { get; set; }
        //public virtual SysDepartment Parent { get; set; }
        
    }
}
