using System;
using System.Collections.Generic;

namespace practice2.Models;

public partial class District
{
    public int DistrictId { get; set; }

    public string? DistrictName { get; set; }

    public virtual ICollection<EstateObject> EstateObjects { get; set; } = new List<EstateObject>();
}
