using System;
using System.Collections.Generic;

namespace SistemaWebDisbofar.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public string Codigo { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public int? IdCategoria { get; set; }

    public int? IdLaboratorio { get; set; }

    public bool? RequiereReceta { get; set; }

    public bool? Estado { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Laboratorio? IdLaboratorioNavigation { get; set; }

    public virtual ICollection<Lote> Lotes { get; set; } = new List<Lote>();
}
