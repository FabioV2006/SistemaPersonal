using System;
using System.Collections.Generic;

namespace SistemaWebDisbofar.Models;

public partial class DetalleDevolucionesProveedore
{
    public int IdDetalleDevolucionProveedor { get; set; }

    public int? IdDevolucionProveedor { get; set; }

    public int? IdLote { get; set; }

    public int? Cantidad { get; set; }

    public decimal? MontoUnitario { get; set; }

    public decimal? SubTotal { get; set; }

    public virtual DevolucionesProveedore? IdDevolucionProveedorNavigation { get; set; }

    public virtual Lote? IdLoteNavigation { get; set; }
}
