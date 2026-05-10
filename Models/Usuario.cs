using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ViziLogin.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        // Adicionamos esta linha para o banco de dados gerar o número sozinho (1, 2, 3...)
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUsuario { get; set; }

        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Senha { get; set; }

        // Mantenha o nome exatamente como no formulário HTML (Tipo_Perfil)
        public string? Tipo_Perfil { get; set; }

        // O int? já permite que o valor seja nulo no banco de dados
        [Column("Id_Area")]
        public int? Id_Area { get; set; }
    }
}