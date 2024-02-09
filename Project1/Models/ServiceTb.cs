using System;
using System.Collections.Generic;

namespace Project1.Models;

public partial class ServiceTb
{
    public long Id { get; set; }

    public string? LongDesc { get; set; }

    public double ServiceCharge { get; set; }

    public string? ServiceName { get; set; }

    public double ServiceTax { get; set; }

    public string? ShortDesc { get; set; }

    public virtual ICollection<EmployeeTb> EmployeeTbs { get; set; } = new List<EmployeeTb>();

    public virtual ICollection<OrdersTb> OrdersTbs { get; set; } = new List<OrdersTb>();

    public virtual ServiceImg? ServiceImg { get; set; }
}
