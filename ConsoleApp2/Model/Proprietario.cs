namespace ConsoleApp2.Model
{
    public class Proprietario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public int? CodStatus { get; set; }

        public Proprietario(string nome, string email)
        {
            this.Nome = nome;
            this.Email = email;
        }
    }
}
