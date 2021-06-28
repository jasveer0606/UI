using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using UI.Models;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static List<Employee> empList = new List<Employee>();
        private readonly IConfiguration configuration;
        private readonly string connectionString;

        public HomeController(IConfiguration config, ILogger<HomeController> logger)
        {
            configuration = config;
            _logger = logger;
            connectionString = configuration.GetConnectionString("DefaultConnectionString");
        }

        [HttpGet]
        public IActionResult EmpCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EmpCreate(Employee emp)
        {
            try
            {
                if (empList.Count == 0 || empList.FirstOrDefault(x => x.EmpID == emp.EmpID) == null)
                {
                    empList.Add(emp);
                    SqlConnection connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand sqlCommand = new SqlCommand("INSERT INTO Employee VALUES " +
                        "(" + Convert.ToInt32(emp.EmpID) + "," +
                        "'" + emp.EmpDepartment + "'" + "," +
                        "'" + emp.EmpName + "'" + "," +
                        +emp.EmpSalary +
                        ")", connection);
                    sqlCommand.ExecuteScalar();
                    connection.Close();
                    return RedirectToAction("EmpListView", "Home");
                }
                else
                {
                    ModelState.AddModelError(nameof(Employee.EmpID), "Employee Id already exists");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult EmpListView()
        {
            try
            {
                empList.Clear();
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("Select * from Employee", connection);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Employee employee = new Employee
                    {
                        EmpID = Convert.ToInt32(reader["EmpID"]),
                        EmpDepartment = ChooseDepartment(reader["EmpDepartment"].ToString()),
                        EmpName = reader["EmpName"].ToString(),
                        EmpSalary = Convert.ToDecimal(reader["EmpSalary"]),
                    };
                    empList.Add(employee);
                }
                connection.Close();
                return View(empList);
            }
            catch
            {
                return View(empList);
            }
        }

        [HttpPost]
        public IActionResult EmpListView(IFormCollection collection)
        {
            try
            {
                bool check = false;
                foreach (var key in collection.Keys)
                {
                    if (string.IsNullOrEmpty(collection[key.ToString()]))
                    {
                        check = true;
                    }
                }
                if (check)
                {
                    return RedirectToAction("EmpListView", "Home");
                }
                Employee employee = new Employee
                {
                    EmpID = Convert.ToInt32(collection["item.EmpId"]),
                    EmpDepartment = ChooseDepartment(collection["item.EmpDepartment"].ToString()),
                    EmpName = collection["item.EmpName"].ToString(),
                    EmpSalary = Convert.ToDecimal(collection["item.EmpSalary"])
                };
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand("UPDATE Employee SET " +
                    "EmpDepartment='" + employee.EmpDepartment.ToString() + "'" + "," +
                    "EmpName='" + employee.EmpName + "'" + "," +
                    "EmpSalary=" + employee.EmpSalary +
                    "WHERE EmpId =" + employee.EmpID, connection);
                sqlCommand.ExecuteNonQuery();
                connection.Close();
                return RedirectToAction("EmpListView", "Home");
            }
            catch
            {
                return View(empList);
            }
        }

        private Department ChooseDepartment(string dept)
        {
            switch (dept)
            {
                case "Accounting": return Department.Accounting;
                case "HR": return Department.HR;
                case "Inventory": return Department.Inventory;
                default: return Department.IT;
            }
        }

        [HttpGet]
        public IActionResult EmpSalary()
        {
            return View();
        }

        [HttpPost]
        public IActionResult EmpSalary(int empId)
        {
            try
            {
                var employee = empList.FirstOrDefault(x => x.EmpID == empId);
                if (employee != null)
                {
                    FileStream fileStreamPath = new FileStream(@"C:\Users\hp\Desktop\Payslip.dotx"
                                                                , FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (WordDocument document = new WordDocument(fileStreamPath, FormatType.Automatic))
                    {
                        BookmarksNavigator bookmarkNavigator = new BookmarksNavigator(document);
                        bookmarkNavigator.MoveToBookmark("EmployeeID");
                        bookmarkNavigator.InsertText(employee.EmpID.ToString());
                        bookmarkNavigator.MoveToBookmark("EmployeeName");
                        bookmarkNavigator.InsertText(employee.EmpName.ToString());
                        bookmarkNavigator.MoveToBookmark("EmployeeDepartment");
                        bookmarkNavigator.InsertText(employee.EmpDepartment.ToString());
                        bookmarkNavigator.MoveToBookmark("TotalAddition");
                        var totalAdd = employee.EmpSalary;
                        bookmarkNavigator.InsertText(totalAdd.ToString());
                        var totalDeduct = Decimal.Multiply(employee.EmpSalary, Convert.ToDecimal(0.02));
                        bookmarkNavigator.MoveToBookmark("TotalDeduction");
                        bookmarkNavigator.InsertText(totalDeduct.ToString());
                        bookmarkNavigator.MoveToBookmark("TotalSalary");
                        bookmarkNavigator.InsertText((totalAdd - totalDeduct).ToString());
                        document.EnsureMinimal();
                        MemoryStream stream = new MemoryStream();
                        document.Save(stream, FormatType.Docx);
                        stream.Position = 0;
                        return File(stream, "application/msword", employee.EmpID.ToString() + "_" + employee.EmpName.ToString() + "_Payslip.docx");
                    }
                }
                else
                {
                    ModelState.AddModelError(nameof(Employee.EmpID), "Could not find Employee");
                }
                return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
