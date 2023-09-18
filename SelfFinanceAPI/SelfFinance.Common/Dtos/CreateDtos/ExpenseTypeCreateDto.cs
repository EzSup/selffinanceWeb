using System.Text.Json.Serialization;

namespace SelfFinanceCommon.Dtos.CreateDtos
{
    public class ExpenseTypeCreateDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isIncome")]
        public bool IsIncome { get; set; }
    }
}
