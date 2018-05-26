using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Modelo.Models;

namespace Repositorio
{
    public partial class UDPUBLISHContext : DbContext
    {
        public virtual DbSet<Cv> Cv { get; set; }
        public virtual DbSet<EstadoPrereg> EstadoPrereg { get; set; }
        public virtual DbSet<Evaluacion> Evaluacion { get; set; }
        public virtual DbSet<Permisos> Permisos { get; set; }
        public virtual DbSet<Preregistros> Preregistros { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<TipoPreregistro> TipoPreregistro { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }

        public UDPUBLISHContext(DbContextOptions<UDPUBLISHContext> options)
                            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cv>(entity =>
            {
                entity.ToTable("CV");

                entity.Property(e => e.CvId)
                    .HasColumnName("cv_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CvAdjunto)
                    .IsRequired()
                    .HasColumnName("cv_adjunto");
            });

            modelBuilder.Entity<EstadoPrereg>(entity =>
            {
                entity.HasKey(e => e.EstprId);

                entity.ToTable("ESTADO_PREREG");

                entity.Property(e => e.EstprId)
                    .HasColumnName("estpr_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.EstprNombre)
                    .IsRequired()
                    .HasColumnName("estpr_nombre")
                    .HasColumnType("char(10)");
            });

            modelBuilder.Entity<Evaluacion>(entity =>
            {
                entity.HasKey(e => e.EvalId);

                entity.ToTable("EVALUACION");

                entity.Property(e => e.EvalId).HasColumnName("eval_id");

                entity.Property(e => e.EvalObservacion)
                    .IsRequired()
                    .HasColumnName("eval_observacion")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PreregId).HasColumnName("prereg_id");

                entity.Property(e => e.UsrId).HasColumnName("usr_id");

                entity.HasOne(d => d.Prereg)
                    .WithMany(p => p.Evaluacion)
                    .HasForeignKey(d => d.PreregId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVALUACION_PREREGISTROS");

                entity.HasOne(d => d.Usr)
                    .WithMany(p => p.Evaluacion)
                    .HasForeignKey(d => d.UsrId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EVALUACION_USUARIO");
            });

            modelBuilder.Entity<Permisos>(entity =>
            {
                entity.HasKey(e => e.PermId);

                entity.ToTable("PERMISOS");

                entity.Property(e => e.PermId)
                    .HasColumnName("perm_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.PermModulo)
                    .IsRequired()
                    .HasColumnName("perm_modulo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RolId).HasColumnName("rol_id");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Permisos)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PERMISOS_ROLES");
            });

            modelBuilder.Entity<Preregistros>(entity =>
            {
                entity.HasKey(e => e.PreregId);

                entity.ToTable("PREREGISTROS");

                entity.Property(e => e.PreregId)
                    .HasColumnName("prereg_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.CvId).HasColumnName("cv_id");

                entity.Property(e => e.EstprId).HasColumnName("estpr_id");

                entity.Property(e => e.PreregApellidos)
                    .IsRequired()
                    .HasColumnName("prereg_apellidos")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PreregAreaProfesional)
                    .HasColumnName("prereg_area_profesional")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PreregEmail)
                    .IsRequired()
                    .HasColumnName("prereg_email")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PreregFechaCreacion)
                    .HasColumnName("prereg_fecha_creacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.PreregFechaModificacion)
                    .HasColumnName("prereg_fecha_modificacion")
                    .HasColumnType("datetime");

                entity.Property(e => e.PreregIdentificacion)
                    .IsRequired()
                    .HasColumnName("prereg_identificacion")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.PreregNombres)
                    .IsRequired()
                    .HasColumnName("prereg_nombres")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PreregTematica)
                    .HasColumnName("prereg_tematica")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.TipoprId).HasColumnName("tipopr_id");

                entity.HasOne(d => d.Cv)
                    .WithMany(p => p.Preregistros)
                    .HasForeignKey(d => d.CvId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PREREGISTROS_CV");

                entity.HasOne(d => d.Estpr)
                    .WithMany(p => p.Preregistros)
                    .HasForeignKey(d => d.EstprId)
                    .HasConstraintName("FK_PREREGISTROS_ESTADO_PREREG");

                entity.HasOne(d => d.Tipopr)
                    .WithMany(p => p.Preregistros)
                    .HasForeignKey(d => d.TipoprId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PREREGISTROS_TIPO_PREREGISTRO");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RolId);

                entity.ToTable("ROLES");

                entity.Property(e => e.RolId)
                    .HasColumnName("rol_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.RolNombre)
                    .IsRequired()
                    .HasColumnName("rol_nombre")
                    .HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<TipoPreregistro>(entity =>
            {
                entity.HasKey(e => e.TipoprId);

                entity.ToTable("TIPO_PREREGISTRO");

                entity.Property(e => e.TipoprId)
                    .HasColumnName("tipopr_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.TipoprNombre)
                    .IsRequired()
                    .HasColumnName("tipopr_nombre")
                    .HasColumnType("char(15)");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsrId);

                entity.ToTable("USUARIO");

                entity.Property(e => e.UsrId)
                    .HasColumnName("usr_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.RolId).HasColumnName("rol_id");

                entity.Property(e => e.UsrActivo).HasColumnName("usr_activo");

                entity.Property(e => e.UsrNombre)
                    .IsRequired()
                    .HasColumnName("usr_nombre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsrPassword)
                    .IsRequired()
                    .HasColumnName("usr_password")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Usuario)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_USUARIO_ROLES");
            });
        }
    }
}
