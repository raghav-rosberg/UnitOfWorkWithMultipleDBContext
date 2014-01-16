namespace UoW_MultipleDBContext.Entity
{
    public class Employee : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
