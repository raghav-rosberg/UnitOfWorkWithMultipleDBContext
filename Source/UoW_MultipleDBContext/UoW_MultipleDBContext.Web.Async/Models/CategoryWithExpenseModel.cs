using System.ComponentModel.DataAnnotations;

namespace UoW_MultipleDBContext.Web.Async.Models
{
    public class CategoryWithExpenseModel
    {
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double TotalExpenses { get; set; }
    }
}