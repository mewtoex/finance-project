using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinanceAPI.DTOs
{
    public class CreateGastoDto
    {
        [Required]
        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [Required]
        [JsonPropertyName("valor")]
        public decimal Valor { get; set; }

        [JsonPropertyName("categoria")]
        public string Categoria { get; set; }

        [Required]
        [JsonPropertyName("data")]
        public DateTime Data { get; set; }
    }
}