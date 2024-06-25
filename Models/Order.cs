using System;
using System.Collections.Generic;

namespace KursachAnna.Models;

public partial class Order
{
    public int Orderid { get; set; }

    public DateOnly Orderdate { get; set; }

    public decimal Totalamount { get; set; }

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}
