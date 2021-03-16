using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(ErrorMessage = "Este Campo deve conter entre 3 e 60 caracter.")]
        [MinLength(3, ErrorMessage = "Este Campo deve conter entre 3 e 60 caracter.")]
        public string Title { get; set; }
    }
}