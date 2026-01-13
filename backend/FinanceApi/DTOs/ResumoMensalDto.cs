namespace FinanceAPI.DTOs
{
    public record CategoriaTotalDto(string Categoria, decimal Total);

    public record ResumoMensalDto(
        string Mes, 
        decimal TotalGasto, 
        List<CategoriaTotalDto> Detalhes
    );
}