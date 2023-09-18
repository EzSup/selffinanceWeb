using System.Text.Json.Serialization;

namespace SelfFinanceCommon.Dtos.CreateDtos
{
    public class FinancialOperationCreateDto
    {
        [JsonPropertyName("typeId")]
        public int TypeId { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("dateTime")]
        public DateTime? DateTime { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }
    }
}
