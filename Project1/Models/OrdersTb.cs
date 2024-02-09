using System;
using System.Collections.Generic;

namespace Project1.Models;

public partial class OrdersTb
{
    public long Id { get; set; }

    public DateTime Bookingtime { get; set; }

    public string? Status { get; set; }

    public long? EmployeeId { get; set; }

    public long? ServiceId { get; set; }

    public long? UserId { get; set; }

    public virtual EmployeeTb? Employee { get; set; }

    public virtual ServiceTb? Service { get; set; }

    public virtual UserTb? User { get; set; }
}
