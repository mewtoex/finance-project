using FinanceAPI.DTOs;
using FinanceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GastoController : ControllerBase
    {
        private readonly IGastoService _service;

        public GastoController(IGastoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CreateGastoDto dto)
        {
            var gasto = await _service.AdicionarGastoAsync(dto);
            return Ok(new { mensagem = "Salvo!", id = gasto.Id });
        }

        [HttpGet("recentes")]
        public async Task<IActionResult> GetRecentes()
        {
            var gastos = await _service.ObterRecentesAsync();
            return Ok(gastos);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] CreateGastoDto dto)
        {
            var sucesso = await _service.AtualizarGastoAsync(id, dto);
            if (!sucesso) return NotFound();
            return Ok(new { mensagem = "Atualizado!" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var sucesso = await _service.DeletarGastoAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }

        [HttpGet("bot/recentes")]
        public async Task<IActionResult> GetRecentesBot()
        {
            var gastos = await _service.ObterRecentesAsync();
            if (!gastos.Any()) return Ok(new { texto = "Nenhum gasto encontrado." });

            var resumo = "📊 *Últimos Gastos:*\n\n";
            foreach (var g in gastos)
            {
                resumo += $"▪️ R$ {g.Valor:F2} - {g.Descricao} ({g.Data:dd/MM})\n";
            }
            return Ok(new { texto = resumo });
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateTime? startDate, [FromQuery] DateTime? endDate)
        {
            var dashboard = await _service.ObterResumoAsync(startDate, endDate);
            
            return Ok(dashboard);
        }
    }
}