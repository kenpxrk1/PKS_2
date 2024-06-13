using System;
using System.Collections.Generic;

namespace practice2.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int? ObjectId { get; set; }

    public DateOnly? DateOfGrade { get; set; }

    public int? ParamId { get; set; }

    public int? Grade1 { get; set; }

    public virtual EstateObject? Object { get; set; }

    public virtual GradeParameter? Param { get; set; }
}
