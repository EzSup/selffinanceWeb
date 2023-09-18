using System.Text.Json.Serialization;

namespace SelfFinanceCommon.Dtos.ForCreate
{
    public class ExpenseTypeForCreateDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isIncome")]
        public bool IsIncome { get; set; }
    }
}
