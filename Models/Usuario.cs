using System;
using System.Collections.Generic;

namespace SistemaWebDisbofar.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public int? IdRol { get; set; }

    public string Documento { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public bool? Estado { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual ICollection<DevolucionesCliente> DevolucionesClientes { get; set; } = new List<DevolucionesCliente>();

    public virtual ICollection<DevolucionesProveedore> DevolucionesProveedores { get; set; } = new List<DevolucionesProveedore>();

    public virtual Role? IdRolNavigation { get; set; }

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
