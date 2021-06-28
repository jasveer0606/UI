using System.ComponentModel.DataAnnotations;

namespace UI
{
    public class Employee
    {
        [Required(ErrorMessage = "Employee ID is required")]
        [Display(Name = "Employee ID")]
        public double EmpID { get; set; }
        [Required(ErrorMessage = "Employee Department is required")]
        [Display(Name = "Department")]
        public Department EmpDepartment { get; set; }
        [Required(ErrorMessage = "Employee Name is required")]
        [Display(Name = "Name")]
        public string EmpName { get; set; }
        [Required(ErrorMessage = "Employee Salary is required")]
        [Display(Name = "Yearly Salary")]
        public decimal EmpSalary { get; set; }
    }
    public enum Department
    {
        HR,
        Accounting,
        IT,
        Inventory
    }
}
