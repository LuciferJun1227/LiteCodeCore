namespace LuckyCode.Entity.OauthBase
{
    public class SysRoleModules
    {
        public virtual string RoleId { get; set; }
        public virtual string ModuleId { get; set; }
        public string ApplicationId { get; set; }
        public string ControllerName { get; set; }
        public long PurviewSum { get; set; }
    }
}
