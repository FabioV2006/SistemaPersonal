using System;
using System.Collections.Generic;

namespace SistemaWebDisbofar.Models;

public partial class DetalleVenta
{
    public int IdDetalleVenta { get; set; }

    public int? IdVenta { get; set; }

    public int? IdLote { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioVentaUnitario { get; set; }

    public decimal? SubTotal { get; set; }

    public virtual Lote? IdLoteNavigation { get; set; }

    public virtual Venta? IdVentaNavigation { get; set; }
}
