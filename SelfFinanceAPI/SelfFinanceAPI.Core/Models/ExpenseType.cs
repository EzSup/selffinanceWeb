using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SelfFinanceAPI.Core.Models
{
    [Table("ExpeseTypes")]
    public class ExpenseType
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("IsIncome")]
        public bool IsIncome { get; set; }
    }
}
