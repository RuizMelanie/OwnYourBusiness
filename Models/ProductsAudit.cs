using System;
using System.Collections.Generic;

namespace ProyectoWebApiRest.Models;

public partial class ProductsAudit
{
    public int? ProductId { get; set; }

    public int ChangeId { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Operation { get; set; }
}
