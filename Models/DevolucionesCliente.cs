using System;
using System.Collections.Generic;

namespace SistemaWebDisbofar.Models;

public partial class DevolucionesCliente
{
    public int IdDevolucionCliente { get; set; }

    public int? IdVenta { get; set; }

    public int? IdUsuario { get; set; }

    public string? Motivo { get; set; }

    public decimal? MontoTotalDevuelto { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<DetalleDevolucionesCliente> DetalleDevolucionesClientes { get; set; } = new List<DetalleDevolucionesCliente>();

    public virtual Usuario? IdUsuarioNavigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}
