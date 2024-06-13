using System;
using System.Collections.Generic;

namespace practice2.Models;

public partial class BuildingMaterial
{
    public int MaterialId { get; set; }

    public string? MaterialName { get; set; }

    public virtual ICollection<EstateObject> EstateObjects { get; set; } = new List<EstateObject>();
}
