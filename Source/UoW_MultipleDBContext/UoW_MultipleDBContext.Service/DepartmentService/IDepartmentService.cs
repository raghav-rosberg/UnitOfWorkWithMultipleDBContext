using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Service.DepartmentService
{
    public interface IDepartmentService
    {
        IEnumerable<Department> GetAll();
        Department GetById(int id);
        int Insert(Department entity);
        int Update(Department entity);
        int Delete(int id);
        int Delete(Department entity);
        int Delete(Expression<Func<Department, bool>> where);
        Department Get(Expression<Func<Department, bool>> where);
        IEnumerable<Department> GetMany(Expression<Func<Department, bool>> where);


        Task<Department> GetByIdAsync(int id);
        Task<List<Department>> GetAllAsync();
        Task<Department> GetAsync(Expression<Func<Department, bool>> where);
        Task<List<Department>> GetManyAsync(Expression<Func<Department, bool>> where);
        Task<int> InsertAsync(Department entity);
        Task<int> UpdateAsync(Department entity);
        Task<int> DeleteAsync(Department entity);
        Task<int> DeleteAsync(int id);
        Task<int> DeleteAsync(Expression<Func<Department, bool>> where);
    }
}