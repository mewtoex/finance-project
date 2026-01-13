using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceAPI.Models
{
    public class Gasto
    {
        [Key]
        public int Id { get; set; }
        
        public string Descricao { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
        
        public string? Categoria { get; set; } 
        public DateTime Data { get; set; } = DateTime.Now; 
    }
}