﻿using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.IdentityModel.Protocols;
using WMS_3PL_IntegrationService.ENTITY;

namespace WMS_3PL_IntegrationService.DAL
{
    public class WMS_3PL_Context : DbContext
    {

        private readonly string connectionString;
        public WMS_3PL_Context() : base()
        {

            this.connectionString = "Data Source=3.224.17.235, 14333;Initial Catalog=BTOB;User ID=icgadmin;Password=masterkey"; 
        }

     
        public DbSet<ENTITY.Articulo> Articulo { get; set; }
        public DbSet<ENTITY.CodigoArticulo> CodigoArticulo { get; set; }
        public DbSet<ENTITY.Cliente> Clientes { get; set; }
        public DbSet<ENTITY.AjusteInventario.AjusteInventario> AjusteInventario { get; set; }
        public DbSet<ENTITY.ConciliacionInventarios.ConciliacionInventario> ConciliacionInventario { get; set; }
        public DbSet<ENTITY.PedidosCompras.PedidosCompra> PedidosCompra { get; set; }
        public DbSet<ENTITY.Precios.PreciosU> Precio { get; set; }
        public DbSet<ENTITY.ConfirmacionPedidoCompra.ConfirmacionPedidoCompra> ConfirmacionPedidoCompra { get; set; }

        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionString, sqlServerOptions => sqlServerOptions.CommandTimeout(680));


            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           
            modelBuilder.Entity<ENTITY.Articulo>().HasNoKey();
            modelBuilder.Entity<ENTITY.CodigoArticulo>().HasNoKey();
            modelBuilder.Entity<ENTITY.Cliente>().HasNoKey();
            modelBuilder.Entity<ENTITY.AjusteInventario.AjusteInventario>().HasNoKey();
            modelBuilder.Entity<ENTITY.ConciliacionInventarios.ConciliacionInventario>().HasNoKey();
           // modelBuilder.Entity<ENTITY.PedidosCompras.PedidosCompra>().HasNoKey();
            modelBuilder.Entity<ENTITY.Precios.PreciosU>().HasNoKey();
            modelBuilder.Entity<ENTITY.ConfirmacionPedidoCompra.ConfirmacionPedidoCompra>().HasNoKey();
            modelBuilder.Entity<ENTITY.PedidosCompras.Detalle>().HasNoKey();
            //modelBuilder.Entity<ENTITY.PedidosCompras.Encabezado>().HasNoKey();






        }
    }
}