using System;
using System.Collections.Generic;
using System.Text;

namespace LuckyCode.Core.Filtes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class ResourceAttribute : Attribute
    {
        private string _resouceName;
        private string _action;
        public ResourceAttribute(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            _resouceName = name;
        }

        public string GetResource()
        {
            return _resouceName;
        }
        public string Action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
            }
        }
    }
    public class ResourceData
    {
        static ResourceData()
        {
            Resources = new List<ResourceEntity>();
        }
        public static void AddResource(string name, ResourceEntity entity)
        {
            if (string.IsNullOrEmpty(name))
            {
                return;
            }
            if (!Resources.Exists(a => a.Area == entity.Area && a.Action == entity.Action && a.Controller == entity.Controller))
            {
                Resources.Add(entity);
            }
        }

        public static List<ResourceEntity> Resources { get; set; }
    }
}
