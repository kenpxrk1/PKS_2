using System;
using System.Collections.Generic;

namespace practice2.Models;

public partial class Realtor
{
    public int RealtorId { get; set; }

    public string? RealtorName { get; set; }

    public string? RealtorLastname { get; set; }

    public string? RealtorSurname { get; set; }

    public string? RealtorPhone { get; set; }

    public virtual ICollection<Selling> Sellings { get; set; } = new List<Selling>();
}
