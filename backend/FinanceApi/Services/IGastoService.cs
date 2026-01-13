using FinanceAPI.DTOs;

namespace FinanceAPI.Services
{
    public interface IGastoService
    {
        Task<GastoResponseDto> AdicionarGastoAsync(CreateGastoDto dto);
        
        Task<List<GastoResponseDto>> ObterRecentesAsync(); 
        
        Task<DashboardDto> ObterDashboardAsync();
        Task<DashboardDto> ObterResumoAsync(DateTime? startDate, DateTime? endDate);
        
        Task<bool> AtualizarGastoAsync(int id, CreateGastoDto dto);
        Task<bool> DeletarGastoAsync(int id);
    }
}