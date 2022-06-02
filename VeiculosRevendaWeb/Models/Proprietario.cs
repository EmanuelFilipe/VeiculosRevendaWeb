using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VeiculosRevendaWeb.Models
{
    public class Proprietario
    {
        public Proprietario(string nome, string documento, string email, string endereco, int? codStatus)
        {
            Nome = nome;
            Documento = documento;
            Email = email;
            Endereco = endereco;
            CodStatus = codStatus;
        }

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Column(TypeName = "nvarchar(50)")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Column(TypeName = "nvarchar(18)")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Column(TypeName = "nvarchar(40)")]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Column(TypeName = "nvarchar(9)")]
        [Display(Name = "CEP")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Status")]
        public int? CodStatus { get; set; }
    }
}
