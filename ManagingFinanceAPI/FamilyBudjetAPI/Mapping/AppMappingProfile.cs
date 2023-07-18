using AutoMapper;
using Budget.BuisnessLogic.Models;
using Domain.BL.Models;
using Domain.DAL.Entity;
using FamilyBudjetAPI.DTOModels;
using ManagingFinanceAPI.DTOModels;

namespace FamilyBudjetAPI.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<CategoryType, CategoryTypeDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<FinanceTransaction, FinanceTransactionDTO>().ReverseMap();
            CreateMap<PeriodReport, PeriodReportDTO>();
            CreateMap<CategoryTypeCollection, CategoryTypeCollectionDTO>().ReverseMap();
            CreateMap<TokenResponse, LoginResponseDTO>().ReverseMap();
            CreateMap<TransactionWithCategoryName, TransactionWithCategoryNameDTO>().ReverseMap();
            CreateMap<BudgetStatisticsData, BudgetStatisticsDataDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}