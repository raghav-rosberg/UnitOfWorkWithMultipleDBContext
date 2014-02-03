using AutoMapper;
using UoW_MultipleDBContext.Entity;
using UoW_MultipleDBContext.Entity.Custom;
using UoW_MultipleDBContext.Web.Async.Models;

namespace UoW_MultipleDBContext.Web.Async.Mappings
{

    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<CategoryModel, Category>();
            Mapper.CreateMap<CategoryWithExpenseModel, CategoryWithExpense>();
            Mapper.CreateMap<DepartmentModel, Department>();  
        }
    }
}