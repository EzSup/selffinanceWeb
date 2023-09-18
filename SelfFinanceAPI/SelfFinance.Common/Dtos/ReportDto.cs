using System.Text.Json.Serialization;

namespace SelfFinanceCommon.Dtos
{
    public class ReportDto
    {
        [JsonPropertyName("startDate")]
        public DateTime startDate { get; set; }
        [JsonPropertyName("endDate")]
        public DateTime endDate { get; set; }
        [JsonPropertyName("totalIncome")]
        public decimal totalIncome { get; set; }
        [JsonPropertyName("totalExpenses")]
        public decimal totalExpenses { get; set; }
        [JsonPropertyName("operations")]
        public List<FinancialOperationDto> operations { get; set; }

        public ReportDto(decimal income, decimal expenses, List<FinancialOperationDto> operations, DateTime startDate, DateTime endDate)
        {
            totalIncome = income;
            totalExpenses = expenses;
            this.startDate = startDate;
            this.endDate = endDate;
            this.operations = operations;
        }
        public ReportDto(decimal income, decimal expenses, List<FinancialOperationDto> operations, DateTime date)
            : this(income, expenses, operations, date, date) { }
        public ReportDto(DateTime date) : this(0, 0, new List<FinancialOperationDto>(), date, date) { }
        public ReportDto(DateTime startDate, DateTime endDate) : this(0, 0, new List<FinancialOperationDto>(), startDate, endDate) { }

        public static ReportDto operator +(ReportDto left, ReportDto right)
        {
            left.totalIncome += right.totalIncome;
            left.totalExpenses += right.totalExpenses;
            left.operations.AddRange(right.operations);
            return left;
        }
    }
}
