using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using LuckyCode.Core.WebSocketChat;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LuckyCode.Core.WebSocket
{
    public static class Extensions
    {
        public static IApplicationBuilder MapWebSocketManager(this IApplicationBuilder app, PathString path, WebSocketHandler handler)
        {
            return app.Map(path, (_app) => _app.UseMiddleware<WebSocketMiddleware>(handler));
        }

        public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
        {
            var handlerBaseType = typeof(WebSocketHandler);

            foreach (var type in Assembly.Load(new AssemblyName("LuckyCode.Core")).ExportedTypes)
            {
                if (type.GetTypeInfo().BaseType == handlerBaseType)
                {
                    services.AddSingleton(type);
                }
            }

            return services;
        }
    }
}
