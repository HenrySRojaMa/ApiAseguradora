﻿using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models.Entities;
using Models.Responses;

namespace Data
{
    public partial class SegurosContext : DbContext
    {
        public SegurosContext()
        {
        }

        public SegurosContext(DbContextOptions<SegurosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Aseguradora> Aseguradoras { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Contrato> Contratos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("conexion"));
            }
        }
        public async Task<Response> ConnectDatabaseAsync()
        {
            Response response = new Response();
            var connectionString = Environment.GetEnvironmentVariable("conexion");

            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                if (connection.State == ConnectionState.Open)
                {
                    response.Code = "00";
                    response.Data = connection;
                }
                else
                {
                    response.Code = "01";
                    response.Message = "No existe conexión con la base de datos. Por favor comuníquese con Soporte.";
                }
            }
            catch (Exception ex)
            {
                response = RspHandler.BadResponse(ex, this.GetType().Name, nameof(ConnectDatabaseAsync));
            }
            return response;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Aseguradora>(entity =>
            {
                entity.HasKey(e => e.IdSeguro);

                entity.ToTable("Aseguradora");

                entity.Property(e => e.IdSeguro).ValueGeneratedNever();

                entity.Property(e => e.Cobertura).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Codigo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prima).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente);

                entity.ToTable("Cliente");

                entity.Property(e => e.IdCliente).ValueGeneratedNever();

                entity.Property(e => e.Cedula)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Nombre)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Contrato>(entity =>
            {
                entity.HasKey(e => e.IdContrato);

                entity.ToTable("Contrato");

                entity.Property(e => e.IdContrato).ValueGeneratedNever();

                entity.Property(e => e.Estado)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.IdCliente)
                    .HasConstraintName("FK_Contrato_Cliente");

                entity.HasOne(d => d.IdSeguroNavigation)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.IdSeguro)
                    .HasConstraintName("FK_Contrato_Aseguradora");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
