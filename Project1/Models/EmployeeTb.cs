using System;
using System.Collections.Generic;

namespace Project1.Models;

public partial class EmployeeTb
{
    public long Id { get; set; }

    public string? DeptName { get; set; }

    public string? EmpStatus { get; set; }

    public string? FirstName { get; set; }

    public DateOnly? HireDate { get; set; }

    public string? LastName { get; set; }

    public long PhoneNum { get; set; }

    public double Salary { get; set; }

    public long? Serviceid { get; set; }

    public virtual ICollection<OrdersTb> OrdersTbs { get; set; } = new List<OrdersTb>();

    public virtual ServiceTb? Service { get; set; }
}
