using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using UoW_MultipleDBContext.Data.DBContexts;
using UoW_MultipleDBContext.Data.UnitOfWork;
using UoW_MultipleDBContext.Entity;

namespace UoW_MultipleDBContext.Service.DepartmentService
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork<SecondDbContext> _unitOfWork;

        public DepartmentService(IUnitOfWork<SecondDbContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Department> GetAll()
        {
            var ds = _unitOfWork.DepartmentRepository.GetAll();
            _unitOfWork.Commit();
            return ds;
        }

        public Department GetById(int id)
        {
            return _unitOfWork.DepartmentRepository.GetById(id);
        }

        public void Insert(Department entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.DepartmentRepository.Insert(entity);
            _unitOfWork.Commit();
        }

        public void Update(Department entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.DepartmentRepository.Update(entity);
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _unitOfWork.DepartmentRepository.GetById(id);
        }

        public void Delete(Department entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.DepartmentRepository.Delete(entity);
            _unitOfWork.Commit();
        }

        public void Delete(Expression<Func<Department, bool>> @where)
        {
            _unitOfWork.DepartmentRepository.Delete(@where);
        }

        public Department Get(Expression<Func<Department, bool>> @where)
        {
            return _unitOfWork.DepartmentRepository.Get(@where);
        }

        public IEnumerable<Department> GetMany(Expression<Func<Department, bool>> @where)
        {
            return _unitOfWork.DepartmentRepository.GetMany(@where);
        }

        public async Task<int> InsertAsync(Department entity)
        {
            _unitOfWork.DepartmentRepository.Insert(entity);
            return await _unitOfWork.CommitAsync();
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _unitOfWork.DepartmentRepository.GetAllAsync();
        }

        public async Task<Department> GetAsync(Expression<Func<Department, bool>> @where)
        {
            return await _unitOfWork.DepartmentRepository.GetAsync(@where);
        }

        public async Task<List<Department>> GetManyAsync(Expression<Func<Department, bool>> @where)
        {
            return await _unitOfWork.DepartmentRepository.GetManyAsync(@where);
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _unitOfWork.DepartmentRepository.GetAsync(department => department.Id == id);
        }
    }
}
