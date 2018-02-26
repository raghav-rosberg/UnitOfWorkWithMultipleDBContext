using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UoW_MultipleDBContext.Service.IService;

namespace UoW_MultipleDBContext.Service
{
    public class CompanyManager
    {
        private ICategoryService categoryService;
        private IDepartmentService departmentService;
        public CompanyManager(ICategoryService categoryService, IDepartmentService departmentService)
        {
            this.categoryService = categoryService;
            this.departmentService = departmentService;
        }
        public void FindCategoryAndCreateDepartment()
        {
            this.categoryService.CallRightOne();
            this.departmentService.CreateNewDepartment("test");
        }
    }
}
