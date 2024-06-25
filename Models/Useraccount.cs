using System;
using System.Collections.Generic;

namespace KursachAnna.Models;

public partial class Useraccount
{
    public int Userid { get; set; }

    public string Username { get; set; } = null!;

    public string Userpassword { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public virtual ICollection<Orderdetail> Orderdetails { get; set; } = new List<Orderdetail>();
}
