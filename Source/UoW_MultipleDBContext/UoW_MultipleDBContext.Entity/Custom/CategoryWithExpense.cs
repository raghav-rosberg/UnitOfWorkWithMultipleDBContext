namespace UoW_MultipleDBContext.Entity.Custom
{
    public class CategoryWithExpense
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public double TotalExpenses { get; set; }
    }
}