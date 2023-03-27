using System;
using System.Collections.Generic;

namespace ProyectoWebApiRest.Models;

public partial class BajaDeStock
{
    public int? ProductId { get; set; }

    public DateTime? FechaBaja { get; set; }
}
