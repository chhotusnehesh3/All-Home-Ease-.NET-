using System;
using System.Collections.Generic;

namespace Project1.Models;

public partial class FeedbackTb
{
    public long Id { get; set; }

    public string? EmailId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Message { get; set; }

    public string? Title { get; set; }
}
