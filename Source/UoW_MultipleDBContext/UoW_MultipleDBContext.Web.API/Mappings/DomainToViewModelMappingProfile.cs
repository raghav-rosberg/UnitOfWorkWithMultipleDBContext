using AutoMapper;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Entity.Custom;
using UoW_MultipleDBContext.Web.API.Models;

namespace UoW_MultipleDBContext.Web.API.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Category, CategoryModel>();
            Mapper.CreateMap<CategoryWithExpense, CategoryWithExpenseModel>();
            Mapper.CreateMap<Department, DepartmentModel>();
        }
    }
}