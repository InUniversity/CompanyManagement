//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompanyManagement.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class SalaryRecord
    {
        public string ID { get; set; }
        public string EmployeeID { get; set; }
        public Nullable<System.DateTime> MonthYear { get; set; }
        public Nullable<int> TotalWorkdays { get; set; }
        public Nullable<decimal> TotalBonus { get; set; }
        public Nullable<decimal> Income { get; set; }
    }
}
