using System;
using System.Collections.Generic;
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

        public void Delete(Department entity)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            _unitOfWork.DepartmentRepository.Delete(entity);
            _unitOfWork.Commit();
        }
    }
}
