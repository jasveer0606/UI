# UI

This project is an MVC core project.<br>
It works on a model whose structure is as follows<br>
Employee : Class<br>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EmpId : double<br>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EmpDepartment : Department<br>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EmpName : string<br>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;EmpSalary : decimal<br>
Department : enum<br>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HR<br>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Accounting<br>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;IT<br>
          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Inventory<br>
This project consist of Employee CUD Operations.<br>
Data is stored using ADO.NET in sqlexpress.<br>
Alongside above mentioned it also has the functionality to download payslip in word .docx format.<br>
The project requires the Payslip.dotx file to be at path "C:\Users\hp\Desktop\Payslip.dotx"
