using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace LuckyCode.Core.Filtes
{
    public class ResourceApplicationModelProvider : IApplicationModelProvider
    {
        public int Order => 10;

        public void OnProvidersExecuted(ApplicationModelProviderContext context)
        {

        }

        public void OnProvidersExecuting(ApplicationModelProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            foreach (var controllerModel in context.Result.Controllers)
            {

                var resourceData = controllerModel.Attributes.OfType<AreaAttribute>().ToArray();
                foreach (var actionModel in controllerModel.Actions)
                {
                    var actionResourceData = actionModel.Attributes.OfType<ResourceAttribute>().ToArray();

                    if (actionResourceData.Length > 0)
                    {
                        var entity = new ResourceEntity();

                        if (resourceData.Length > 0)
                        {
                            entity.Area = resourceData[0].RouteValue;
                        }
                        entity.Controller = controllerModel.ControllerName;
                        entity.Action = actionModel.ActionName;
                        entity.Name = actionResourceData[0].GetResource();
                        ResourceData.AddResource(entity.Area + "-" + entity.Controller, entity);
                    }
                }
            }
        }
    }
}
