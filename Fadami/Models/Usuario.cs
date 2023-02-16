using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Fadami.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "O Nome é obrigatório")]
        [Column("NOME")]
        [MaxLength(50)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Login é obrigatório")]
        [Column("LOGIN")]
        [MaxLength(20)]
        public string Login { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [Column("CPF")]
        [MaxLength(14)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [Column("SENHA")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A {0} deve ter no mínimo {2} e no máximo {1} caracteres")]
        public string Senha { get; set; }

        public DateTime UltimoAcesso { get; set; }


        public int QtdErroLogin { get; set; }

        public bool BLAtivo { get; set; } = true;

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }

        public override string ToString()
        {
            return $"Código: {Codigo}, Nome: {Nome}, Login: {Login}, CPF: {CPF}, Último Acesso: {UltimoAcesso}, Qtd Erro Login: {QtdErroLogin}, BL Ativo: {BLAtivo}";
        }

    }
}