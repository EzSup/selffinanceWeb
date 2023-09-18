using System.Text.Json.Serialization;

namespace SelfFinanceCommon.Dtos
{
    public class ExpenseTypeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isIncome")]
        public bool IsIncome { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as ExpenseTypeDto;
            return other?.Id == Id;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public override string ToString() => Name;

        public string ToShortData() => String.Concat(Name, "/", IsIncome ? "прибуток" : "витрати");
    }
}
