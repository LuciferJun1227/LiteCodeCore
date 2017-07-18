using System;
using System.Collections.Generic;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LuckyCode.Core.WebSocket
{
    public abstract class WebSocketHandler
    {
        private ILogger _logger;
        public WebSocketHandler(ILogger<WebSocketHandler> logger)
        {
            _logger = logger;
        }
        protected abstract int BufferSize { get; }

        private List<WebSocketConnection> _connections = new List<WebSocketConnection>();

        public List<WebSocketConnection> Connections { get => _connections; }

        public async Task ListenConnection(WebSocketConnection connection)
        {
            

            while (connection.WebSocket.State == WebSocketState.Open)
            {
                try
                {
                    ArraySegment<Byte> buffer = new ArraySegment<byte>(new Byte[1024 * 4]);
                    string serializedInvocationDescriptor = null;
                    WebSocketReceiveResult result = null;
                    using (var ms = new MemoryStream())
                    {
                        do
                        {
                            result = await connection.WebSocket.ReceiveAsync(buffer, CancellationToken.None);
                            ms.Write(buffer.Array, buffer.Offset, result.Count);
                        }
                        while (!result.EndOfMessage);

                        ms.Seek(0, SeekOrigin.Begin);

                        using (var reader = new StreamReader(ms, Encoding.UTF8))
                        {
                            serializedInvocationDescriptor = await reader.ReadToEndAsync();
                        }
                    }

                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        var message = serializedInvocationDescriptor;//Encoding.UTF8.GetString(buffer, 0, result.Count);

                        await connection.ReceiveAsync(message);
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await OnDisconnected(connection);
                    }
                }
                catch (WebSocketException e)
                {
                    Console.WriteLine(e);
                    _logger.LogDebug(e.Message);
                    _logger.LogDebug(e.Source);
                    _logger.LogDebug(e.StackTrace);
                    await OnDisconnected(connection);
                }

            }
        }

        public virtual async Task OnDisconnected(WebSocketConnection connection)
        {
            if (connection != null)
            {
                _connections.Remove(connection);
                await connection.WebSocket.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, null, CancellationToken.None);
                //await connection.WebSocket.CloseAsync(
                //    closeStatus: WebSocketCloseStatus.NormalClosure,
                //    statusDescription: "Closed by the WebSocketHandler",
                //    cancellationToken: CancellationToken.None);
            }
        }

        public abstract Task<WebSocketConnection> OnConnected(HttpContext context);
    }
}
