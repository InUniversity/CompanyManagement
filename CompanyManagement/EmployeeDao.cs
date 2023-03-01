using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement
{
    public class EmployeeDao
    {
        DBConnection dbconnection = new DBConnection();
        public void Add(Employee employee)
        {
            string sqlStr = string.Format("INSERT INTO Employee(employee_id, employee_name, gender, birthday, ssn, phone_number, manager_id, salary) VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})", employee.ID, employee.Name, employee.Gender, employee.Birthday, employee.Ssn, employee.Phone, employee.Mgr_ID, employee.Salary);
            dbconnection.Running(sqlStr);
        }
        public void Delete(Employee employee)
        {
            string sqlStr = string.Format("DELETE FROM Employee WHERE employee_id = {0}", employee.ID);
            dbconnection.Running(sqlStr);
        }
        public void Save(Employee employee)
        {
            string sqlStr = string.Format("UPDATE Employee employee_name = '{1}', gender = '{2}', birthday = '{3}', ssn = '{4}', phone_number = '{5}', manager_id = '{6}', salary = '{7}' WHERE employee_id = '{0}'", employee.ID, employee.Name, employee.Gender, employee.Birthday, employee.Ssn, employee.Phone, employee.Mgr_ID, employee.Salary);
            dbconnection.Running(sqlStr);        
        }
        public DataTable ReturnList()
        {
            string sqlStr = string.Format("SELECT * FROM Employee");
            return dbconnection.ReturnTable(sqlStr);
        }
    }
}
