using System;
using System.Collections.Generic;

namespace SistemaWebDisbofar.Models;

public partial class Proveedore
{
    public int IdProveedor { get; set; }

    public string Documento { get; set; } = null!;

    public string RazonSocial { get; set; } = null!;

    public string? Correo { get; set; }

    public string? Telefono { get; set; }

    public bool? Estado { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
