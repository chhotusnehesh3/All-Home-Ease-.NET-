using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project1.Models;

public partial class UserTb
{
    public long Id { get; set; }

    public string? City { get; set; }

    public string? HouseNo { get; set; }

    public string? Pincode { get; set; }

    public string? State { get; set; }

    public string? Street { get; set; }

    public DateTime Dob { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public long Phone { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<OrdersTb> OrdersTbs { get; set; } = new List<OrdersTb>();
}
