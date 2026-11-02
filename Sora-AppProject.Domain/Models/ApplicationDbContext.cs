using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sora_AppProject.Domain.Models;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cotizacione> Cotizaciones { get; set; }

    public virtual DbSet<Diseno> Disenos { get; set; }

    public virtual DbSet<DisenoTela> DisenoTelas { get; set; }

    public virtual DbSet<PrendasBase> PrendasBases { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SolicitudTalla> SolicitudTallas { get; set; }

    public virtual DbSet<SolicitudesCotizacion> SolicitudesCotizacions { get; set; }

    public virtual DbSet<TallerTela> TallerTelas { get; set; }

    public virtual DbSet<Tallere> Talleres { get; set; }

    public virtual DbSet<Tela> Telas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cotizacione>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cotizaciones_pkey");

            entity.ToTable("cotizaciones");

            entity.HasIndex(e => new { e.SolicitudId, e.TallerId }, "cotizaciones_solicitud_id_taller_id_key").IsUnique();

            entity.HasIndex(e => e.TallerId, "ix_cotizaciones_taller_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'enviada'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha");
            entity.Property(e => e.PlazoDias).HasColumnName("plazo_dias");
            entity.Property(e => e.PrecioUnitario)
                .HasPrecision(10, 2)
                .HasColumnName("precio_unitario");
            entity.Property(e => e.SolicitudId).HasColumnName("solicitud_id");
            entity.Property(e => e.TallerId).HasColumnName("taller_id");
            entity.Property(e => e.Total)
                .HasPrecision(12, 2)
                .HasColumnName("total");

            entity.HasOne(d => d.Solicitud).WithMany(p => p.Cotizaciones)
                .HasForeignKey(d => d.SolicitudId)
                .HasConstraintName("cotizaciones_solicitud_id_fkey");

            entity.HasOne(d => d.Taller).WithMany(p => p.Cotizaciones)
                .HasForeignKey(d => d.TallerId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("cotizaciones_taller_id_fkey");
        });

        modelBuilder.Entity<Diseno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("disenos_pkey");

            entity.ToTable("disenos");

            entity.HasIndex(e => e.ClienteId, "ix_disenos_cliente_id");

            entity.HasIndex(e => e.PrendaBaseId, "ix_disenos_prenda_base_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.ConfiguracionVisor)
                .HasDefaultValueSql("'{}'::jsonb")
                .HasColumnType("jsonb")
                .HasColumnName("configuracion_visor");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(120)
                .HasColumnName("nombre");
            entity.Property(e => e.PrendaBaseId).HasColumnName("prenda_base_id");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Disenos)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("disenos_cliente_id_fkey");

            entity.HasOne(d => d.PrendaBase).WithMany(p => p.Disenos)
                .HasForeignKey(d => d.PrendaBaseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("disenos_prenda_base_id_fkey");
        });

        modelBuilder.Entity<DisenoTela>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("diseno_telas_pkey");

            entity.ToTable("diseno_telas");

            entity.HasIndex(e => new { e.DisenoId, e.Parte }, "diseno_telas_diseno_id_parte_key").IsUnique();

            entity.HasIndex(e => e.TelaId, "ix_diseno_telas_tela_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.DisenoId).HasColumnName("diseno_id");
            entity.Property(e => e.Parte)
                .HasMaxLength(30)
                .HasColumnName("parte");
            entity.Property(e => e.TelaId).HasColumnName("tela_id");

            entity.HasOne(d => d.Diseno).WithMany(p => p.DisenoTelas)
                .HasForeignKey(d => d.DisenoId)
                .HasConstraintName("diseno_telas_diseno_id_fkey");

            entity.HasOne(d => d.Tela).WithMany(p => p.DisenoTelas)
                .HasForeignKey(d => d.TelaId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("diseno_telas_tela_id_fkey");
        });

        modelBuilder.Entity<PrendasBase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("prendas_base_pkey");

            entity.ToTable("prendas_base");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .HasColumnName("categoria");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Modelo3dUrl)
                .HasMaxLength(500)
                .HasColumnName("modelo_3d_url");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.Nombre, "roles_nombre_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<SolicitudTalla>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("solicitud_tallas_pkey");

            entity.ToTable("solicitud_tallas");

            entity.HasIndex(e => new { e.SolicitudId, e.Talla }, "solicitud_tallas_solicitud_id_talla_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.SolicitudId).HasColumnName("solicitud_id");
            entity.Property(e => e.Talla)
                .HasMaxLength(10)
                .HasColumnName("talla");

            entity.HasOne(d => d.Solicitud).WithMany(p => p.SolicitudTallas)
                .HasForeignKey(d => d.SolicitudId)
                .HasConstraintName("solicitud_tallas_solicitud_id_fkey");
        });

        modelBuilder.Entity<SolicitudesCotizacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("solicitudes_cotizacion_pkey");

            entity.ToTable("solicitudes_cotizacion");

            entity.HasIndex(e => e.ClienteId, "ix_solicitudes_cliente_id");

            entity.HasIndex(e => e.DisenoId, "ix_solicitudes_diseno_id");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.DisenoId).HasColumnName("diseno_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValueSql("'pendiente'::character varying")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha");

            entity.HasOne(d => d.Cliente).WithMany(p => p.SolicitudesCotizacions)
                .HasForeignKey(d => d.ClienteId)
                .HasConstraintName("solicitudes_cotizacion_cliente_id_fkey");

            entity.HasOne(d => d.Diseno).WithMany(p => p.SolicitudesCotizacions)
                .HasForeignKey(d => d.DisenoId)
                .HasConstraintName("solicitudes_cotizacion_diseno_id_fkey");
        });

        modelBuilder.Entity<TallerTela>(entity =>
        {
            entity.HasKey(e => new { e.TallerId, e.TelaId }).HasName("taller_telas_pkey");

            entity.ToTable("taller_telas");

            entity.HasIndex(e => e.TelaId, "ix_taller_telas_tela_id");

            entity.Property(e => e.TallerId).HasColumnName("taller_id");
            entity.Property(e => e.TelaId).HasColumnName("tela_id");
            entity.Property(e => e.PrecioPorMetro)
                .HasPrecision(10, 2)
                .HasColumnName("precio_por_metro");

            entity.HasOne(d => d.Taller).WithMany(p => p.TallerTelas)
                .HasForeignKey(d => d.TallerId)
                .HasConstraintName("taller_telas_taller_id_fkey");

            entity.HasOne(d => d.Tela).WithMany(p => p.TallerTelas)
                .HasForeignKey(d => d.TelaId)
                .HasConstraintName("taller_telas_tela_id_fkey");
        });

        modelBuilder.Entity<Tallere>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("talleres_pkey");

            entity.ToTable("talleres");

            entity.HasIndex(e => e.UsuarioId, "talleres_usuario_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(120)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .HasColumnName("telefono");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithOne(p => p.Tallere)
                .HasForeignKey<Tallere>(d => d.UsuarioId)
                .HasConstraintName("talleres_usuario_id_fkey");
        });

        modelBuilder.Entity<Tela>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("telas_pkey");

            entity.ToTable("telas");

            entity.HasIndex(e => e.Nombre, "telas_nombre_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Composicion)
                .HasMaxLength(150)
                .HasColumnName("composicion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usuarios_pkey");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.RolId, "ix_usuarios_rol_id");

            entity.HasIndex(e => e.Email, "usuarios_email_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("now()")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .HasColumnName("nombre");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.RolId).HasColumnName("rol_id");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("usuarios_rol_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
