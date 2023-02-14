using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fadami.Models
{
    public class UsuarioSemSenhaModel
    {
        [Key]
        public int Codigo { get; set; }

        public string Nome { get; set; }

        [Required]
        [Column("LOGIN")]
        [MaxLength(20)]
        public string Login { get; set; }

        [Required]
        [Column("CPF")]
        [MaxLength(14)]
        public string CPF { get; set; }

        [Required]
        [Column("SENHA")]
        [MaxLength(20)]
        public string Senha { get; set; }

        public DateTime UltimoAcesso { get; set; }


        public int QtdErroLogin { get; set; }

        public bool BLAtivo { get; set; } = true;
    }
}