using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeiculosRevendaWeb.Models
{
    public class Veiculo
    {
        public Veiculo()
        {

        }

        public Veiculo(string renavam, string modelo, DateTime? anoFabricacao, DateTime? anoModelo, 
                       string quilometragem, string valor, int? codStatus, int? proprietarioId, int? marcaId)
        {
            Renavam = renavam;
            Modelo = modelo;
            AnoFabricacao = anoFabricacao;
            AnoModelo = anoModelo;
            Quilometragem = quilometragem;
            Valor = valor;
            CodStatus = codStatus;
            this.proprietarioId = proprietarioId;
            this.marcaId = marcaId;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Column(TypeName = "nvarchar(11)")]
        [Display(Name = "RENAVAM")]
        public string Renavam { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Column(TypeName = "nvarchar(30)")]
        public string Modelo { get; set; }

        [Display(Name ="Ano Fabricação")]
        public DateTime? AnoFabricacao { get; set; }

        [Display(Name = "Ano Modelo")]
        public DateTime? AnoModelo{ get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Quilometragem { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Column(TypeName = "nvarchar(25)")]
        public string Valor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Status")]
        public int? CodStatus { get; set; }

        [Display(Name = "Proprietário")]
        public int? proprietarioId { get; set; }
        public virtual Proprietario Proprietario { get; set; }

        [Display(Name = "Marca")]
        public int? marcaId { get; set; }
    }
}
