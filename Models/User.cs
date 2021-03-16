using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(3, ErrorMessage = "Este Campo deve conter entre 3 e 20 caracter.")]
        [MaxLength(20, ErrorMessage = "Este Campo deve conter entre 3 e 20 caracter.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MinLength(3, ErrorMessage = "Este Campo deve conter entre 3 e 20 caracter.")]
        [MaxLength(20, ErrorMessage = "Este Campo deve conter entre 3 e 20 caracter.")]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}