using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using VeiculosRevendaWeb.Data.DTO;

namespace VeiculosRevendaWeb.RabbitMQSender
{
    public class RabbitMQMessageSender : IRabbitMQMessageSender
    {
        private readonly string _hostName;
        private readonly string _password;
        private readonly string _userName;
        private IConnection _connection;

        public RabbitMQMessageSender()
        {
            _hostName = "localhost";
            _password = "guest";
            _userName = "guest";
        }

        public void SendMessage(BaseMessage baseMessage, string queueName)
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostName,
                Password = _password,
                UserName = _userName
            };

            _connection = factory.CreateConnection();

            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
                byte[] body = GetMessageAsByteArray(baseMessage);

                channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body);
            };
        }

        private byte[] GetMessageAsByteArray(BaseMessage message)
        {
            // para serealizar as classes filhas
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var json = JsonSerializer.Serialize<VeiculoDTO>((VeiculoDTO)message, options);
            var body = Encoding.UTF8.GetBytes(json);

            return body;
        }
    }
}
