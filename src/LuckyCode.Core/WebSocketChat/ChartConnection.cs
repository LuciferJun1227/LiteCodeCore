using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuckyCode.Core.WebSocket;
using Newtonsoft.Json;

namespace LuckyCode.Core.WebSocketChat
{
    public class ChartConnection : WebSocketConnection
    {
        public ChartConnection(WebSocketHandler handler) : base(handler)
        {
        }
        public Guid Id { get; set; }
        public string NickName { get; set; }

        public override async Task ReceiveAsync(string message)
        {
            var receiveMessage = JsonConvert.DeserializeObject<ReceiveMessage>(message);

            var receiver = Handler.Connections.FirstOrDefault(m => ((ChartConnection)m).NickName == receiveMessage.Receiver);

            if (receiver != null)
            {
                var sendMessage = JsonConvert.SerializeObject(new SendMessage
                {
                    Sender = NickName,
                    Message = receiveMessage.Message
                });

                await receiver.SendMessageAsync(sendMessage);
            }
            else
            {
                var sendMessage = JsonConvert.SerializeObject(new SendMessage
                {
                    Sender = NickName,
                    Message = receiveMessage.Message
                });
                foreach (var conn in Handler.Connections)
                {
                    await conn.SendMessageAsync(sendMessage);
                }

            }
        }

        private class ReceiveMessage
        {
            public string Receiver { get; set; }

            public string Message { get; set; }
        }

        private class SendMessage
        {
            public string Sender { get; set; }

            public string Message { get; set; }
        }
    }
}
