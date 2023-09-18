using AutoMapper;
using SelfFinanceAPI.Core.Models;
using SelfFinanceCommon.Dtos;

namespace SelfFinanceAPI.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ExpenseType, ExpenseTypeDto>();
            CreateMap<FinancialOperation, FinancialOperationDto>(); 
        }
        
    }
}
