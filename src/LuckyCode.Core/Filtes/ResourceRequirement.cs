using Microsoft.AspNetCore.Authorization;

namespace LuckyCode.Core.Filtes
{
    public static class Policies
    {
        public const string CanViewUsers = "CanViewUsers";
        
    }
    public class ResourceEntity
    {
        public string Name { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
    public class ResourceRequirement: ResourceEntity,IAuthorizationRequirement
    {
        public ResourceRequirement() { }
        public ResourceRequirement(string area, string controller, string action)
        {
            Area = area;
            Controller = controller;
            Action = action;
        }
        
    }
}
