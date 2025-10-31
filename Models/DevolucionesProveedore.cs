using System;
using System.Collections.Generic;

namespace SistemaWebDisbofar.Models;

public partial class DevolucionesProveedore
{
    public int IdDevolucionProveedor { get; set; }

    public int? IdCompra { get; set; }

    public int? IdUsuario { get; set; }

    public string? Motivo { get; set; }

    public decimal? MontoTotalDevuelto { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleDevolucionesProveedore> DetalleDevolucionesProveedores { get; set; } = new List<DetalleDevolucionesProveedore>();

    public virtual Compra? IdCompraNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
