//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CompanyManagementEntity
{
    using System;
    using System.Collections.Generic;
    
    public partial class Task
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> Deadline { get; set; }
        public string Progress { get; set; }
        public string OwnerID { get; set; }
        public string EmployeeID { get; set; }
        public string ProjectID { get; set; }
        public Nullable<int> StatusID { get; set; }
    }
}
