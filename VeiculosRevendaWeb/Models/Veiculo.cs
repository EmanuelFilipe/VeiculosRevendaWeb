using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosRevendaWeb.Models
{
    public class Veiculo
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Column(TypeName = "nvarchar(11)")]
        [Display(Name = "RENAVAM")]
        public string Renavam { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Column(TypeName = "nvarchar(30)")]
        public string Modelo { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name ="Ano Fabricação")]
        public DateTime? AnoFabricacao { get; set; }

        //[Required(ErrorMessage = "O campo {0} é obrigatório")]
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

        [Display(Name = "Marca")]
        public int? marcaId { get; set; }
    }
}
