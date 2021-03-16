using System.ComponentModel.DataAnnotations;

namespace Shop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [MaxLength(ErrorMessage = "Este Campo deve conter entre 3 e 60 caracter.")]
        [MinLength(3, ErrorMessage = "Este Campo deve conter entre 3 e 60 caracter.")]
        public string Title { get; set; }

        [MaxLength(1024, ErrorMessage = "O campo possui capacidade maxima de 1024 caracter.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "O valor tem que ser maior que 0")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria Invalida.")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

    }
}