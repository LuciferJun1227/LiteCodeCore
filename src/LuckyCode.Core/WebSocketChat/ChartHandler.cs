using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LuckyCode.Core.WebSocket;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace LuckyCode.Core.WebSocketChat
{
    public class ChartHandler : WebSocketHandler
    {
        private ILogger _logger;
        public ChartHandler(ILogger<WebSocketHandler> logger) : base(logger)
        {
            _logger = logger;
        }

        protected override int BufferSize { get => 1024 * 4; }

        public override async Task<WebSocketConnection> OnConnected(HttpContext context)
        {
            var name = context.Request.Query["Name"];
            if (!string.IsNullOrEmpty(name))
            {
                var connection = Connections.FirstOrDefault(m => ((ChartConnection) m).NickName == name);
                if (connection != null)
                {
                    Connections.Remove(connection);
                }
                var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                connection = new ChartConnection(this)
                {
                    NickName = name,
                    WebSocket = webSocket
                };
                Connections.Add(connection);
                foreach (var conn in Connections)
                {
                    await conn.SendMessageAsync(JsonConvert.SerializeObject(new
                    {
                        Sender = name,
                        Message = "上线了"
                    }));
                }
                return connection;
            }
            return null;
        }
    }
}
