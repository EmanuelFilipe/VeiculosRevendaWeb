using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosRevendaWeb.Models
{
    public class Marca
    {
        public Marca(string nome, int? codStatus)
        {
            Nome = nome;
            CodStatus = codStatus;
        }

        public Marca()
        {
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Column(TypeName = "nvarchar(50)")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Status")]
        public int? CodStatus { get; set; }
    }
}
