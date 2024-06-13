using System;
using System.Collections.Generic;

namespace practice2.Models;

public partial class EstateObject
{
    public int ObjectId { get; set; }

    public int? District { get; set; }

    public string? Adress { get; set; }

    public int? Floorr { get; set; }

    public int? QuantityOfRooms { get; set; }

    public int? Types { get; set; }

    public int? Status { get; set; }

    public float? Price { get; set; }

    public string? EstateObjectsDescription { get; set; }

    public int? EstateObjectsMaterial { get; set; }

    public float? EstateObjectsSquare { get; set; }

    public DateOnly? AdDate { get; set; }

    public virtual District? DistrictNavigation { get; set; }

    public virtual BuildingMaterial? EstateObjectsMaterialNavigation { get; set; }

    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

    public virtual ICollection<Selling> Sellings { get; set; } = new List<Selling>();

    public virtual Type? TypesNavigation { get; set; }
}
