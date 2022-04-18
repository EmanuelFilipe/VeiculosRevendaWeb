using RabbitMQ_Consumer.Model;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RabbitMQ_Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            ReceiveMessage();
        }

        private static void ReceiveMessage()
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {

                    channel.QueueDeclare(
                        queue: "veiculoQueue",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine("[x] Received {0}", message);
                        Console.WriteLine();

                        getProprietario(message);
                    };

                    channel.BasicConsume(
                        queue: "veiculoQueue",
                        autoAck: true,
                        consumer: consumer);

                    Console.WriteLine("Pres [enter] to exit.");
                    Console.WriteLine();
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static void getProprietario(string message)
        {
            Veiculo veiculo = JsonConvert.DeserializeObject<Veiculo>(message);
            sendEmail(veiculo.Proprietario);
        }

        private static void sendEmail(Proprietario proprietario)
        {
            if (proprietario != null)
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("filipesilvabr2@gmail.com");
                mailMessage.To.Add(proprietario.Email);
                mailMessage.Subject = "Cadastro de Veículo";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = TemplateEmail($"Olá <b>{proprietario.Nome}</b>, seu veículo foi cadastrado com sucesso!");

                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new NetworkCredential("filipesilvabr2@gmail.com", "senha");

                    try
                    {
                        smtpClient.Send(mailMessage);
                        Console.WriteLine("E-mail enviado com sucesso!");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw e;
                    }
                }
            }
        }

        private static string TemplateEmail(string texto)
        {
            string template = @"<html><body style='font-family: 'Source Sans Pro', 'Helvetica Neue', Helvetica, Arial, sans-serif; width: 600px;'>
                                    {0}
                                </body></html>";

            return string.Format(template, texto);
        }
    }
}
