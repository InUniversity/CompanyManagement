//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

public partial class Project
{
    public string ID { get; set; }
    public string ProjectName { get; set; }
    public string Details { get; set; }
    public Nullable<System.DateTime> CreatedDate { get; set; }
    public Nullable<System.DateTime> StartDate { get; set; }
    public Nullable<System.DateTime> EndDate { get; set; }
    public Nullable<System.DateTime> CompletedDate { get; set; }
    public string Progress { get; set; }
    public Nullable<int> StatusID { get; set; }
    public string OwnerID { get; set; }
    public Nullable<decimal> BonusSalary { get; set; }
}
