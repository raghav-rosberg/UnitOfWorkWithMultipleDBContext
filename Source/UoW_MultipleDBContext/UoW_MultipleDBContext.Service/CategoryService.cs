using UoW_MultipleDBContext.Data.Repositories;
using UoW_MultipleDBContext.Service.IService;

namespace UoW_MultipleDBContext.Service
{
    public class CategoryService :ICategoryService
    {
        //private IUnitOfWork<FirstDbContext> unitOfWork;
        private IDBOneRepositories categoryRepositories;
        public CategoryService(IDBOneRepositories categoryRepositories)
        {
            //this.unitOfWork = unitOfWork;
            this.categoryRepositories = categoryRepositories;
        }
        public void CallRightOne()
        {
            var xx = this.categoryRepositories.CategoryRepository.GetAll();
        }
    }
}
