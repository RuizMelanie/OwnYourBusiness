using System;
using System.Collections.Generic;

namespace ProyectoWebApiRest.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Usuarios { get; set; }

    public string Clave { get; set; }

    public virtual ICollection<HistorialRefreshToken> HistorialRefreshTokens { get; } = new List<HistorialRefreshToken>();
}
