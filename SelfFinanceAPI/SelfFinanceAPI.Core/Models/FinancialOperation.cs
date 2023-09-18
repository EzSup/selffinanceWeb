using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfFinanceAPI.Core.Models
{
    [Table("FinancialOperations")]
    public class FinancialOperation
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("TypeId")]
        public int TypeId { get; set; }

        [Column("Amount")]
        public decimal Amount { get; set; }

        [Column("DateTime")]
        public DateTime DateTime { get; set; }

        [Column("Description")]
        public string? Description { get; set; }
    }
}
