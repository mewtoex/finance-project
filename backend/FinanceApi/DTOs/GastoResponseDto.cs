using System.Text.Json.Serialization;

namespace FinanceAPI.DTOs
{
    public class GastoResponseDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; }

        [JsonPropertyName("valor")]
        public decimal Valor { get; set; }

        [JsonPropertyName("categoria")]
        public string Categoria { get; set; }

        [JsonPropertyName("data")]
        public DateTime Data { get; set; }
    }
}