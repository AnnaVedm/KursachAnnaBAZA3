using System;
using System.Collections.Generic;

namespace KursachAnna.Models;

public partial class Category
{
    public int Categoryid { get; set; }

    public string Categoryname { get; set; } = null!;

    public virtual ICollection<Categories2> Categories2s { get; set; } = new List<Categories2>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
