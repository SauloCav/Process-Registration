using System;
using System.ComponentModel.DataAnnotations;

namespace ProcessRegistration.Models
{
    public class ProcessModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do processo é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome do processo não pode exceder 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O NPU é obrigatório.")]
        [RegularExpression(@"^\d{7}-\d{2}\.\d{4}\.\d{1}\.\d{2}\.\d{4}$", ErrorMessage = "O NPU deve estar no formato 1111111-11.1111.1.11.1111.")]
        public string NPU { get; set; }

        [Required(ErrorMessage = "A data de cadastro é obrigatória.")]
        [Display(Name = "Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Data de Visualização")]
        public DateTime? DataVisualizacao { get; set; }

        [Required(ErrorMessage = "O UF é obrigatório.")]
        [StringLength(2, ErrorMessage = "O UF deve conter exatamente 2 caracteres.")]
        public string UF { get; set; }

        [Required(ErrorMessage = "O município é obrigatório.")]
        public string Municipio { get; set; }

        [Required(ErrorMessage = "O código do município é obrigatório.")]
        public int CodigoMunicipio { get; set; }
    }
}
