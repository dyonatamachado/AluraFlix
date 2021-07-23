using System.ComponentModel.DataAnnotations;

namespace AluraFlix.Models
{
    public class UpdateVideo
    {
        [Required]
        [StringLength(100, ErrorMessage = "O Título é obrigatório e deve conter no máximo 100 caracteres.")]
        public string Titulo { get; set; }
        [Required]
        [StringLength(2000, ErrorMessage = "A Descrição é obrigatória e deve conter no máximo 2000 caracteres.")]
        public string Descricao { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "A URL é obrigatória e deve conter no máximo 200 caracteres.")]
        public string Url { get; set; }
    }
}