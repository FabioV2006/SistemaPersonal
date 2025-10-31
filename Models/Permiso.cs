using System;
using System.Collections.Generic;

namespace SistemaWebDisbofar.Models;

public partial class Permiso
{
    public int IdPermiso { get; set; }

    public int? IdRol { get; set; }

    public string NombreMenu { get; set; } = null!;

    public virtual Role? IdRolNavigation { get; set; }
}
