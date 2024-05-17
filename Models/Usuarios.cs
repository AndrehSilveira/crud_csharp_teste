using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
  [Table("usuarios")]
  public class Usuarios
  {
    [Column("id")]
    public int Id { get; set; }

    [Column("nome")]
    public string? Nome { get; set; }

    [Column("senha")]
    public string? Senha { get; set; }

    [Column("status")]
    public bool Status { get; set; }
  }

  public class UsuarioDTOAlterar
    {
        public int Id { get; set;}
        public string? Senha { get; set; }
        public bool Status { get; set; }
    }

  public class UsuarioResposta
  {
    public string? Sucesso { get; set; }
  }
}