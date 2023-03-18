using System.Data;

namespace CompanyManagement.Database
{
    public class EmployeeDao
    {
        
        private const string TABLE_NAME = "Employee";
        public const string ID = "employee_id";
        public const string NAME = "employee_name";
        public const string GENDER = "gender";
        public const string BIRTHDAY = "birthday";
        public const string IDENTIFY_CARD = "identify_card";
        public const string EMAIL = "email";
        public const string PHONE_NUMBER = "phone_number";
        public const string ADDRESS = "employee_address";
        public const string DEPARTMENT_ID = "department_id";
        public const string POSITION_ID = "position_id";
        public const string SALARY = "salary";

        private DBConnection dbconnection = new DBConnection();

        public void Add(Employee empl)
        {
            string sqlStr =
                $"INSERT INTO {TABLE_NAME} ({ID}, {NAME}, {GENDER}, {BIRTHDAY}, {IDENTIFY_CARD}, {EMAIL}, " +
                $"{PHONE_NUMBER}, {ADDRESS}, {DEPARTMENT_ID},{POSITION_ID}, {SALARY}) " +
                $"VALUES ('{empl.ID}', N'{empl.Name}', N'{empl.Gender}', '{empl.Birthday}', " +
                $"'{empl.IdentifyCard}', '{empl.Email}', '{empl.PhoneNumber}', N'{empl.Address}', " +
                $"'{empl.DepartmentID}', '{empl.PositionID}', '{empl.Salary}')";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public void Delete(string id)
        {
            string sqlStr = $"DELETE FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public void Update(Employee empl)
        {
            string sqlStr = $"UPDATE {TABLE_NAME}" +
                $"SET {NAME}=N'{empl.Name}', {GENDER}=N'{empl.Gender}', {BIRTHDAY}='{empl.Birthday}', " +
                $"{IDENTIFY_CARD}='{empl.IdentifyCard}', {EMAIL}={empl.Email}, " +
                $"{PHONE_NUMBER}='{empl.PhoneNumber}', {ADDRESS}={empl.Address}, {DEPARTMENT_ID}='{empl.DepartmentID}', " +
                $"{POSITION_ID}='{empl.PositionID}', {SALARY}='{empl.Salary}' WHERE {ID}='{empl.ID}'";
            dbconnection.ExecuteNonQuery(sqlStr);
        }

        public DataTable GetDataTable()
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME}";
            return dbconnection.GetDataTable(sqlStr);
        }

        public Employee SearchByID(string id)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {ID} = '{id}'";
            DataTable dataTable = dbconnection.GetDataTable(sqlStr);
            if (dataTable.Rows.Count == 0)
                return null;
            return new Employee(dataTable.Rows[0]);
        }

        public Employee SearchByIdentifyCard(string identifyCard)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {IDENTIFY_CARD} = '{identifyCard}'";
            DataTable dataTable = dbconnection.GetDataTable(sqlStr);
            if (dataTable.Rows.Count == 0)
                return null;
            return new Employee(dataTable.Rows[0]);
        }

        public Employee SearchByPhoneNumber(string phoneNumber)
        {
            string sqlStr = $"SELECT * FROM {TABLE_NAME} WHERE {PHONE_NUMBER} = '{phoneNumber}'";
            DataTable dataTable = dbconnection.GetDataTable(sqlStr);
            if (dataTable.Rows.Count == 0)
                return null;
            return new Employee(dataTable.Rows[0]);
        }
    }
}
