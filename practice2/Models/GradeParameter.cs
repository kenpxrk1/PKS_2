using System;
using System.Collections.Generic;

namespace practice2.Models;

public partial class GradeParameter
{
    public int ParamId { get; set; }

    public string? ParamName { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
