using System;
using System.Collections.Generic;

namespace SistemaWebDisbofar.Models;

public partial class DetalleCompra
{
    public int IdDetalleCompra { get; set; }

    public int? IdCompra { get; set; }

    public int? IdLote { get; set; }

    public int? Cantidad { get; set; }

    public decimal? PrecioCompraUnitario { get; set; }

    public decimal? MontoTotal { get; set; }

    public virtual Compra? IdCompraNavigation { get; set; }

    public virtual Lote? IdLoteNavigation { get; set; }
}
