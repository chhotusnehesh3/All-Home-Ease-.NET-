using System;
using System.Collections.Generic;

namespace Project1.Models;

public partial class ImgTb
{
    public long Id { get; set; }

    public byte[]? ImpSize { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<ServiceImg> ServiceImgs { get; set; } = new List<ServiceImg>();
}
