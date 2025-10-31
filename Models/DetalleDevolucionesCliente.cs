using System;
using System.Collections.Generic;

namespace SistemaWebDisbofar.Models;

public partial class DetalleDevolucionesCliente
{
    public int IdDetalleDevolucionCliente { get; set; }

    public int? IdDevolucionCliente { get; set; }

    public int? IdLote { get; set; }

    public int? Cantidad { get; set; }

    public decimal? MontoUnitario { get; set; }

    public decimal? SubTotal { get; set; }

    public virtual DevolucionesCliente? IdDevolucionClienteNavigation { get; set; }

    public virtual Lote? IdLoteNavigation { get; set; }
}
