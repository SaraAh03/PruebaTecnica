using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Models;

public partial class AsoConstructorasContext : DbContext
{
    public AsoConstructorasContext()
    {
    }

    public AsoConstructorasContext(DbContextOptions<AsoConstructorasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PersonasInteresada> PersonasInteresadas { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        //=> optionsBuilder.UseSqlServer("server=localhost; database=AsoConstructoras; integrated security=true; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonasInteresada>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personas__3214EC0743FEFE91");

            entity.Property(e => e.Correo).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(255);

            entity.HasOne(d => d.Proyecto).WithMany(p => p.PersonasInteresada)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("FK__PersonasI__Proye__3A81B327");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proyecto__3214EC07B04CB858");

            entity.HasIndex(e => e.Codigo, "UQ__Proyecto__06370DACDD5A0117").IsUnique();

            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Constructora).HasMaxLength(100);
            entity.Property(e => e.Contacto).HasMaxLength(100);
            entity.Property(e => e.Direccion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
