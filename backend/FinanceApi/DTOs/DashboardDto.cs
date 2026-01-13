using System.Text.Json.Serialization; 
namespace FinanceAPI.DTOs
{
    public class DashboardDto
    {
        [JsonPropertyName("totalGasto")]
        public decimal TotalGasto { get; set; }

        [JsonPropertyName("quantidadeTransacoes")]
        public int QuantidadeTransacoes { get; set; }

        [JsonPropertyName("grafico")]
        public List<GraficoItemDto> Grafico { get; set; } = new();
    }

    public class GraficoItemDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        public decimal Value { get; set; }
    }
}