using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SistemaWebDisbofar.Models;

public partial class DistribuidoraDbContext : DbContext
{
    public DistribuidoraDbContext()
    {
    }

    public DistribuidoraDbContext(DbContextOptions<DistribuidoraDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categoria> Categorias { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }

    public virtual DbSet<DetalleDevolucionesCliente> DetalleDevolucionesClientes { get; set; }

    public virtual DbSet<DetalleDevolucionesProveedore> DetalleDevolucionesProveedores { get; set; }

    public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }

    public virtual DbSet<DevolucionesCliente> DevolucionesClientes { get; set; }

    public virtual DbSet<DevolucionesProveedore> DevolucionesProveedores { get; set; }

    public virtual DbSet<Laboratorio> Laboratorios { get; set; }

    public virtual DbSet<Lote> Lotes { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.IdCategoria).HasName("PK__CATEGORI__A3C02A1098A7FD82");

            entity.ToTable("CATEGORIAS");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__CLIENTES__D5946642E1A81CDB");

            entity.ToTable("CLIENTES");

            entity.HasIndex(e => e.Documento, "UQ__CLIENTES__AF73706D704F812F").IsUnique();

            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DireccionEnvio)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.NombreComercial)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__COMPRAS__0A5CDB5C7C41F1E6");

            entity.ToTable("COMPRAS");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .HasConstraintName("FK__COMPRAS__IdProve__5CD6CB2B");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__COMPRAS__IdUsuar__5BE2A6F2");
        });

        modelBuilder.Entity<DetalleCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra).HasName("PK__DETALLE___E046CCBB51A165F9");

            entity.ToTable("DETALLE_COMPRAS");

            entity.Property(e => e.MontoTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioCompraUnitario).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DETALLE_C__IdCom__60A75C0F");

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.DetalleCompras)
                .HasForeignKey(d => d.IdLote)
                .HasConstraintName("FK__DETALLE_C__IdLot__619B8048");
        });

        modelBuilder.Entity<DetalleDevolucionesCliente>(entity =>
        {
            entity.HasKey(e => e.IdDetalleDevolucionCliente).HasName("PK__DETALLE___3F9232930AEE57AE");

            entity.ToTable("DETALLE_DEVOLUCIONES_CLIENTES");

            entity.Property(e => e.MontoUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdDevolucionClienteNavigation).WithMany(p => p.DetalleDevolucionesClientes)
                .HasForeignKey(d => d.IdDevolucionCliente)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DETALLE_D__IdDev__71D1E811");

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.DetalleDevolucionesClientes)
                .HasForeignKey(d => d.IdLote)
                .HasConstraintName("FK__DETALLE_D__IdLot__72C60C4A");
        });

        modelBuilder.Entity<DetalleDevolucionesProveedore>(entity =>
        {
            entity.HasKey(e => e.IdDetalleDevolucionProveedor).HasName("PK__DETALLE___961D048937F77804");

            entity.ToTable("DETALLE_DEVOLUCIONES_PROVEEDORES");

            entity.Property(e => e.MontoUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdDevolucionProveedorNavigation).WithMany(p => p.DetalleDevolucionesProveedores)
                .HasForeignKey(d => d.IdDevolucionProveedor)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DETALLE_D__IdDev__7A672E12");

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.DetalleDevolucionesProveedores)
                .HasForeignKey(d => d.IdLote)
                .HasConstraintName("FK__DETALLE_D__IdLot__7B5B524B");
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DETALLE___AAA5CEC266D3A366");

            entity.ToTable("DETALLE_VENTAS");

            entity.Property(e => e.PrecioVentaUnitario).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SubTotal).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdLoteNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdLote)
                .HasConstraintName("FK__DETALLE_V__IdLot__6A30C649");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DETALLE_V__IdVen__693CA210");
        });

        modelBuilder.Entity<DevolucionesCliente>(entity =>
        {
            entity.HasKey(e => e.IdDevolucionCliente).HasName("PK__DEVOLUCI__5C45015DB8BA67B2");

            entity.ToTable("DEVOLUCIONES_CLIENTES");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MontoTotalDevuelto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Motivo)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.DevolucionesClientes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__DEVOLUCIO__IdUsu__6E01572D");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DevolucionesClientes)
                .HasForeignKey(d => d.IdVenta)
                .HasConstraintName("FK__DEVOLUCIO__IdVen__6D0D32F4");
        });

        modelBuilder.Entity<DevolucionesProveedore>(entity =>
        {
            entity.HasKey(e => e.IdDevolucionProveedor).HasName("PK__DEVOLUCI__6EF266E7408DC22B");

            entity.ToTable("DEVOLUCIONES_PROVEEDORES");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MontoTotalDevuelto).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Motivo)
                .HasMaxLength(250)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.DevolucionesProveedores)
                .HasForeignKey(d => d.IdCompra)
                .HasConstraintName("FK__DEVOLUCIO__IdCom__75A278F5");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.DevolucionesProveedores)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__DEVOLUCIO__IdUsu__76969D2E");
        });

        modelBuilder.Entity<Laboratorio>(entity =>
        {
            entity.HasKey(e => e.IdLaboratorio).HasName("PK__LABORATO__FC9B92174A8F9643");

            entity.ToTable("LABORATORIOS");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Lote>(entity =>
        {
            entity.HasKey(e => e.IdLote).HasName("PK__LOTES__38C4EE90986D87F4");

            entity.ToTable("LOTES");

            entity.HasIndex(e => new { e.IdProducto, e.NumeroLote }, "UQ__LOTES__CBC385747ABC9AB2").IsUnique();

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.NumeroLote)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PrecioCompra).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PrecioVenta).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UbicacionAlmacen)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.Lotes)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__LOTES__IdProduct__571DF1D5");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__PERMISOS__0D626EC83D6CFD75");

            entity.ToTable("PERMISOS");

            entity.Property(e => e.NombreMenu)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Permisos)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__PERMISOS__IdRol__3F466844");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__PRODUCTO__098892100C4C74D6");

            entity.ToTable("PRODUCTOS");

            entity.HasIndex(e => e.Codigo, "UQ__PRODUCTO__06370DACE00FF8FF").IsUnique();

            entity.Property(e => e.Codigo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Descripcion)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombre)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.RequiereReceta).HasDefaultValue(false);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("FK__PRODUCTOS__IdCat__5070F446");

            entity.HasOne(d => d.IdLaboratorioNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdLaboratorio)
                .HasConstraintName("FK__PRODUCTOS__IdLab__5165187F");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__PROVEEDO__E8B631AFC6549175");

            entity.ToTable("PROVEEDORES");

            entity.HasIndex(e => e.Documento, "UQ__PROVEEDO__AF73706D63F58159").IsUnique();

            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__ROLES__2A49584C3DBE7794");

            entity.ToTable("ROLES");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__USUARIOS__5B65BF9791C20FE8");

            entity.ToTable("USUARIOS");

            entity.HasIndex(e => e.Correo, "UQ__USUARIOS__60695A199F502F6B").IsUnique();

            entity.HasIndex(e => e.Documento, "UQ__USUARIOS__AF73706D6EBA2309").IsUnique();

            entity.Property(e => e.Clave)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Documento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__USUARIOS__IdRol__3B75D760");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__VENTAS__BC1240BDADC91A3B");

            entity.ToTable("VENTAS");

            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MontoCambio).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MontoPago).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MontoTotal).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdCliente)
                .HasConstraintName("FK__VENTAS__IdClient__656C112C");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__VENTAS__IdUsuari__6477ECF3");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
