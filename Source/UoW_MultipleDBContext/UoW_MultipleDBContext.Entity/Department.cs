using System.Collections.Generic;

namespace UoW_MultipleDBContext.Entity
{
    public class Department : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
