using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace SelfFinanceCommon.Dtos
{
    public class FinancialOperationDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("typeId")]
        public int TypeId { get; set; }
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        [JsonPropertyName("dateTime")]
        public DateTime? DateTime { get; set; }
        [JsonPropertyName("description")]
        public string? Description { get; set; }

        public string ToShortData() => String.Concat(((DateTime)DateTime).ToString(Constants.DateFormat), "/", Amount, "/", Description);
    }
}
