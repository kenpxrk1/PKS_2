using System;
using System.Collections.Generic;

namespace practice2.Models;

public partial class Selling
{
    public int SellingId { get; set; }

    public int? ObjectId { get; set; }

    public DateOnly? SellingDate { get; set; }

    public int? RealtorId { get; set; }

    public float? Price { get; set; }

    public virtual EstateObject? Object { get; set; }

    public virtual Realtor? Realtor { get; set; }
}
