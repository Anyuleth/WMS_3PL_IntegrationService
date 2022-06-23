using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.IdentityModel.Protocols;
using WMS_3PL_IntegrationService.ENTITY;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Configuration;

namespace WMS_3PL_IntegrationService.DAL
{
    public class WMS_3PL_Context : DbContext
    {

        private readonly string connectionString;
        public WMS_3PL_Context() : base()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();

            this.connectionString = ConfigurationManager.ConnectionStrings["BTOB"].ToString();
        }

        public WMS_3PL_Context(string connectionString) : base()
        {
            this.connectionString = connectionString;
        }


        public DbSet<ENTITY.Articulo> Articulo { get; set; }
        public DbSet<ENTITY.CodigoArticulo> CodigoArticulo { get; set; }
        public DbSet<ENTITY.Cliente> Clientes { get; set; }
        public DbSet<ENTITY.AjusteInventario.AjusteInventario> AjusteInventario { get; set; }
        public DbSet<ENTITY.ConciliacionInventarios.ConciliacionInventario> ConciliacionInventario { get; set; }
        public DbSet<ENTITY.PedidosCompras.Encabezaddo> PedidosCompraEncabezado { get; set; }
        public DbSet<ENTITY.PedidosCompras.Lineas> Detalle { get; set; }
        public DbSet<ENTITY.Precios.PreciosU> Precio { get; set; }
        public DbSet<ENTITY.ConfirmacionPedidoCompra.ConfirmacionPedidoCompra> ConfirmacionPedidoCompra { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(680000));


            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           
            modelBuilder.Entity<ENTITY.Articulo>().HasNoKey();
            modelBuilder.Entity<ENTITY.CodigoArticulo>().HasNoKey();
            modelBuilder.Entity<ENTITY.Cliente>().HasNoKey();
            modelBuilder.Entity<ENTITY.AjusteInventario.AjusteInventario>().HasNoKey();
            modelBuilder.Entity<ENTITY.ConciliacionInventarios.ConciliacionInventario>().HasNoKey();
            modelBuilder.Entity<ENTITY.Precios.PreciosU>().HasNoKey();
            modelBuilder.Entity<ENTITY.ConfirmacionPedidoCompra.ConfirmacionPedidoCompra>().HasNoKey();
            modelBuilder.Entity<ENTITY.PedidosCompras.Encabezaddo>().HasKey(x => new { x.Documento, x.Empresa, x.Tipo });
            modelBuilder.Entity<ENTITY.PedidosCompras.Lineas>().HasKey(x => new { x.linea_Numero, x.Producto });







        }
    }
}
