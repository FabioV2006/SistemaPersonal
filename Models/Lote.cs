using System;
using System.Collections.Generic;

namespace SistemaWebDisbofar.Models;

public partial class Lote
{
    public int IdLote { get; set; }

    public int? IdProducto { get; set; }

    public string NumeroLote { get; set; } = null!;

    public DateOnly FechaVencimiento { get; set; }

    public int Stock { get; set; }

    public decimal PrecioCompra { get; set; }

    public decimal PrecioVenta { get; set; }

    public string? UbicacionAlmacen { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual ICollection<DetalleDevolucionesCliente> DetalleDevolucionesClientes { get; set; } = new List<DetalleDevolucionesCliente>();

    public virtual ICollection<DetalleDevolucionesProveedore> DetalleDevolucionesProveedores { get; set; } = new List<DetalleDevolucionesProveedore>();

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } = new List<DetalleVenta>();

    public virtual Producto? IdProductoNavigation { get; set; }
}
