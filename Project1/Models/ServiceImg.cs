using System;
using System.Collections.Generic;

namespace Project1.Models;

public partial class ServiceImg
{
    public long? ImgId { get; set; }

    public long ServiceId { get; set; }

    public virtual ImgTb? Img { get; set; }

    public virtual ServiceTb Service { get; set; } = null!;
}
