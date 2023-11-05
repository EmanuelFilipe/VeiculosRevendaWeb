using System;
using VeiculosRevendaWeb.Models;
using VeiculosRevendaWeb.RabbitMQSender;

namespace VeiculosRevendaWeb.Data.DTO
{
    public class VeiculoDTO : BaseMessage
    {
        public string Renavam { get; set; }
        public string Modelo { get; set; }
        public DateTime? AnoFabricacao { get; set; }
        public DateTime? AnoModelo { get; set; }
        public string Quilometragem { get; set; }
        public string Valor { get; set; }
        public int? CodStatus { get; set; }
        public int? proprietarioId { get; set; }
        public virtual Proprietario Proprietario { get; set; }
        public int? marcaId { get; set; }
    }
}
