using System;

namespace RabbitMQ_Consumer.Model
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Renavam { get; set; }
        public string Modelo { get; set; }
        public DateTime? AnoFabricacao { get; set; }
        public DateTime? AnoModelo{ get; set; }
        public string Quilometragem { get; set; }
        public string Valor { get; set; }
        public int? CodStatus { get; set; }
        public int? proprietarioId { get; set; }
        public virtual Proprietario Proprietario { get; set; }
        public int? marcaId { get; set; }
    }
}
