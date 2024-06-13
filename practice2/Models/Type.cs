using System;
using System.Collections.Generic;

namespace practice2.Models;

public partial class Type
{
    public int TypeId { get; set; }

    public char? TypeName { get; set; }

    public virtual ICollection<EstateObject> EstateObjects { get; set; } = new List<EstateObject>();
}
