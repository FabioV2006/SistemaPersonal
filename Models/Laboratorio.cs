using System;
using System.Collections.Generic;

namespace SistemaWebDisbofar.Models;

public partial class Laboratorio
{
    public int IdLaboratorio { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
