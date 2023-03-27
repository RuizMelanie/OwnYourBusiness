using System;
using System.Collections.Generic;

namespace ProyectoWebApiRest.Models;

public partial class Token
{
    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaExpiracion { get; set; }

    public bool? EsActivo { get; set; }
}
