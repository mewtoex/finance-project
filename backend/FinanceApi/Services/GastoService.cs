using FinanceAPI.Data;
using FinanceAPI.DTOs;
using FinanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceAPI.Services
{
    public class GastoService : IGastoService
    {
        private readonly AppDbContext _context;

        public GastoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GastoResponseDto> AdicionarGastoAsync(CreateGastoDto dto)
        {
            var entity = new Gasto
            {
                Descricao = dto.Descricao,
                Valor = dto.Valor,
                Categoria = dto.Categoria,
                Data = dto.Data
            };

            _context.Gastos.Add(entity);
            await _context.SaveChangesAsync();

            return new GastoResponseDto
            {
                Id = entity.Id,
                Descricao = entity.Descricao,
                Valor = entity.Valor,
                Categoria = entity.Categoria,
                Data = entity.Data
            };
        }

        public async Task<List<GastoResponseDto>> ObterRecentesAsync()
        {
            var gastos = await _context.Gastos
                .OrderByDescending(x => x.Data)
                .Take(20)
                .ToListAsync();

            return gastos.Select(t => new GastoResponseDto
            {
                Id = t.Id,
                Descricao = t.Descricao,
                Valor = t.Valor,
                Categoria = t.Categoria ,
                Data = t.Data
            }).ToList();
        }


        public async Task<DashboardDto> ObterDashboardAsync()
        {
            var inicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var fim = inicio.AddMonths(1).AddDays(-1);
            return await ObterResumoAsync(inicio, fim);
        }

        public async Task<bool> AtualizarGastoAsync(int id, CreateGastoDto dto)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null) return false;

            gasto.Descricao = dto.Descricao;
            gasto.Valor = dto.Valor;
            gasto.Categoria = dto.Categoria;
            gasto.Data = dto.Data;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletarGastoAsync(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null) return false;

            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<DashboardDto> ObterResumoAsync(DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Gastos.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(t => t.Data >= startDate.Value.ToUniversalTime());

            if (endDate.HasValue)
                query = query.Where(t => t.Data <= endDate.Value.ToUniversalTime());

            var totalGasto = await query.SumAsync(t => t.Valor);
            var qtd = await query.CountAsync();

            var grafico = await query
                .GroupBy(t => t.Categoria)
                .Select(g => new GraficoItemDto
                {
                    Name = g.Key ?? "Outros",
                    Value = g.Sum(t => t.Valor)
                })
                .ToListAsync();

            return new DashboardDto
            {
                TotalGasto = Math.Abs(totalGasto),
                QuantidadeTransacoes = qtd,
                Grafico = grafico
            };
        }
    }
}