using System;
using System.Collections.Generic;

namespace KursachAnna.Models;

public partial class Categories2
{
    public int Categoriesid2 { get; set; }

    public string CategoriaName { get; set; } = null!;

    public int? Categoryid1 { get; set; }

    public virtual Category? Categoryid1Navigation { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
