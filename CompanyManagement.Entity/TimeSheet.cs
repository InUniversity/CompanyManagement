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
    
    public partial class TimeSheet
    {
        public string ID { get; set; }
        public System.DateTime CheckInTime { get; set; }
        public Nullable<System.DateTime> CheckOutTime { get; set; }
        public string EmployeeID { get; set; }
        public string TaskCheckInID { get; set; }
    }
}
