using System;

namespace UoW_MultipleDBContext.Entity
{
    public class Expense : BaseEntity<int>
    {     
        public string Transaction { get; set; }       
        public DateTime Date { get; set; }     
        public double Amount { get; set; }      
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
