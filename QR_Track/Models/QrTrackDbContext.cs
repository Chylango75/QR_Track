using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QR_Track.Models;

public partial class QrTrackDbContext : DbContext
{
    public QrTrackDbContext()
    {
    }

    public QrTrackDbContext(DbContextOptions<QrTrackDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblLeido> TblLeidos { get; set; }

    public virtual DbSet<TblPersona> TblPersonas { get; set; }

    public virtual DbSet<TblQr> TblQrs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblLeido>(entity =>
        {
            entity.ToTable("tblLeido");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DtLeido)
                .HasColumnType("datetime")
                .HasColumnName("dtLeido");
            entity.Property(e => e.IdQr).HasColumnName("idQr");
        });

        modelBuilder.Entity<TblPersona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tblEntidad");

            entity.ToTable("tblPersona");

            entity.Property(e => e.Nombre)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblQr>(entity =>
        {
            entity.ToTable("tblQR");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
