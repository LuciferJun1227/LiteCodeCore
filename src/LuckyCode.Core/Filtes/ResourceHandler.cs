using System.Threading.Tasks;
using LiteCode.Core.Filtes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LuckyCode.Core.Filtes
{
    /// <summary>
    /// 基于授权策略对请求的资源授权限制
    /// </summary>
    public class ResourceHandler:AuthorizationHandler<ResourceRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var mvcContext = context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;

                if (mvcContext != null)
                {
                    var route = mvcContext.RouteData;

                    string area = route.Values["area"].ToString();
                    string urlController = route.Values["controller"].ToString();
                    string action = route.Values["action"].ToString();
                     
                    //if (urlController == "Home")
                    //{
                    //    context.Succeed(requirement);
                    //}
                    //else
                    //{
                    //    context.Fail();

                    //}

                }
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public class ValidateAuthorExistsAttribute : TypeFilterAttribute
    {
        public ValidateAuthorExistsAttribute() : base(typeof
          (ValidateAuthorExistsFilterImpl))
        {
        }
        private class ValidateAuthorExistsFilterImpl : IAsyncActionFilter
        {
            private readonly IAuthorRepository _authorRepository;
            public ValidateAuthorExistsFilterImpl(IAuthorRepository authorRepository)
            {
                _authorRepository = authorRepository;
            }
            public async Task OnActionExecutionAsync(ActionExecutingContext context,ActionExecutionDelegate next)
            {
                
                if (context.ActionArguments.ContainsKey("id"))
                {
                    var id = context.ActionArguments["id"] as int?;
                    if (id.HasValue)
                    {
                       // if ((await _authorRepository.ListAsync()).All(a => a.Id != id.Value))
                        {
                            context.Result = new NotFoundObjectResult(id.Value);
                            return;
                        }
                    }
                }
                await next();
            }
        }
    }
}
