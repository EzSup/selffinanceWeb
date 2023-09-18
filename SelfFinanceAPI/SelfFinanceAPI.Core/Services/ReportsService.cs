using SelfFinanceAPI.Core.Models;
using SelfFinanceCommon.Dtos.ForCreate;
using SelfFinanceCommon.Dtos;
using SelfFinanceAPI.Core.Repositories.Interfaces;
using SelfFinanceAPI.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SelfFinanceAPI.Core.Services
{
    public class ReportsService : IReportsService
    {
        private IExpenseTypesRepository _expenseTypesRepository;
        private IFinancialOperationsRepository _financialOpetrationsRepository;
        private IMapper _mapper;

        public ReportsService(IFinancialOperationsRepository financialOpetrationsRepository, IExpenseTypesRepository expenseTypesRepository, IMapper mapper)
        {
            _financialOpetrationsRepository = financialOpetrationsRepository;
            _expenseTypesRepository = expenseTypesRepository;
            _mapper = mapper;
        }

        public async Task<ReportDto> DailyReport(DateTime dateTime)
        {
            return await PeriodReport(dateTime, dateTime);
        }

        public async Task<ReportDto> PeriodReport(DateTime startDate, DateTime endDate)
        {
            ReportDto report = new ReportDto(startDate, endDate);
            report.operations = _mapper.Map<ICollection<FinancialOperationDto>>(await _financialOpetrationsRepository.Get(startDate, endDate)).ToList();
            foreach (var operation in report.operations)
            {
                if (_expenseTypesRepository.Get(operation.TypeId).Result.IsIncome)
                {
                    report.totalIncome += operation.Amount;
                }
                else
                {
                    report.totalExpenses += operation.Amount;
                }
            }
            return report;
        }
    }
}
