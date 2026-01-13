using FinanceAPI.Data;
using FinanceAPI.DTOs;
using FinanceAPI.Models; 
using FinanceAPI.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApi.Tests
{
    public class GastoServiceTests
    {
        private AppDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;
            
            return new AppDbContext(options);
        }

        [Fact]
        public async Task AdicionarGasto_DeveSalvarComDataCorreta()
        {
            var context = GetDatabaseContext();
            var service = new GastoService(context);
            var dataTeste = new DateTime(2025, 10, 15);

            var dto = new CreateGastoDto
            {
                Descricao = "Teste Data",
                Valor = 100,
                Categoria = "Teste",
                Data = dataTeste
            };

            var resultado = await service.AdicionarGastoAsync(dto);

            Assert.NotNull(resultado);
            Assert.Equal(dataTeste, resultado.Data);
            
            // CORREÇÃO: Usar .Gastos (Plural)
            Assert.Equal(1, context.Gastos.Count()); 
        }

        [Fact]
        public async Task ObterDashboard_DeveSomarApenasMesAtual()
        {
            var context = GetDatabaseContext();
            var service = new GastoService(context);

            // CORREÇÃO: Usar .Gastos (Plural)
            context.Gastos.Add(new Gasto 
            { 
                Descricao = "Hoje", 
                Valor = 50, 
                Categoria = "Lazer", 
                Data = DateTime.Now 
            });

            context.Gastos.Add(new Gasto 
            { 
                Descricao = "Antigo", 
                Valor = 1000, 
                Categoria = "Lazer", 
                Data = DateTime.Now.AddMonths(-1) 
            });

            await context.SaveChangesAsync();

            var dashboard = await service.ObterDashboardAsync();

            Assert.Equal(50, dashboard.TotalGasto);
            Assert.Single(dashboard.Grafico); 
        }

        [Fact]
        public async Task ObterResumoAsync_DeveFiltrarPorDataECalcularTotais()
        {
            var context = GetDatabaseContext();
            
            // CORREÇÃO: Usar .Gastos (Plural)
            context.Gastos.AddRange(
                new Gasto { Descricao = "Luz", Valor = 100, Categoria = "Casa", Data = new DateTime(2026, 01, 10) },
                new Gasto { Descricao = "Internet", Valor = 200, Categoria = "Casa", Data = new DateTime(2026, 01, 15) },
                new Gasto { Descricao = "Carro", Valor = 500, Categoria = "Transporte", Data = new DateTime(2026, 02, 01) }
            );
            await context.SaveChangesAsync();

            var service = new GastoService(context);
            
            var inicio = new DateTime(2026, 01, 01);
            var fim = new DateTime(2026, 01, 31);

            var resultado = await service.ObterResumoAsync(inicio, fim);

            Assert.NotNull(resultado);
            Assert.Equal(300, resultado.TotalGasto); 
            Assert.Equal(2, resultado.QuantidadeTransacoes); 
            
            var itemGrafico = resultado.Grafico.FirstOrDefault(x => x.Name == "Casa");
            Assert.NotNull(itemGrafico);
            Assert.Equal(300, itemGrafico.Value);
        }
    }
}