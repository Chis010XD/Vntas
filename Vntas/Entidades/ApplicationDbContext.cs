using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Vntas.Entidades
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();


                var connectionString = configuration.GetConnectionString("cnx");
                optionsBuilder.UseSqlServer(connectionString);
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //              optionsBuilder.UseSqlServer("Data Source=DAVID\\SQLEXPRESS;Initial Catalog=Ventas;MultipleActiveResultSets=True;TrustServerCertificate=True;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("Cliente");

                entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Cliente");

                entity.Property(e => e.TelefonoCliente).HasColumnName("Telefono_Cliente");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura);

                entity.ToTable("Factura");

                entity.Property(e => e.IdFactura)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Factura");

                entity.Property(e => e.FechaFactura)
                    .HasColumnType("date")
                    .HasColumnName("Fecha_Factura");

                entity.Property(e => e.IdProducto).HasColumnName("id_Producto");

                entity.Property(e => e.TotalFactura)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("Total_Factura");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_Factura_Producto");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("Producto");

                entity.Property(e => e.IdProducto)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Producto");

                entity.Property(e => e.IdCliente).HasColumnName("Id_Cliente");

                entity.Property(e => e.NombreProducto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nombre_Producto");

                entity.Property(e => e.PrecioProducto)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("Precio_Producto");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_Producto_Cliente");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
