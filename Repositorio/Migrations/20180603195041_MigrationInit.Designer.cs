﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Repositorio;
using System;

namespace Repositorio.Migrations
{
    [DbContext(typeof(UDPUBLISHContext))]
    [Migration("20180603195041_MigrationInit")]
    partial class MigrationInit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Modelo.Models.Cv", b =>
                {
                    b.Property<int>("CvId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("cv_id");

                    b.Property<byte[]>("CvAdjunto")
                        .IsRequired()
                        .HasColumnName("cv_adjunto");

                    b.HasKey("CvId");

                    b.ToTable("CV");
                });

            modelBuilder.Entity("Modelo.Models.EstadoPrereg", b =>
                {
                    b.Property<int>("EstprId")
                        .HasColumnName("estpr_id");

                    b.Property<string>("EstprNombre")
                        .IsRequired()
                        .HasColumnName("estpr_nombre")
                        .HasColumnType("varchar(100)");

                    b.HasKey("EstprId");

                    b.ToTable("ESTADO_PREREG");
                });

            modelBuilder.Entity("Modelo.Models.Evaluacion", b =>
                {
                    b.Property<int>("EvalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("eval_id");

                    b.Property<string>("EvalObservacion")
                        .IsRequired()
                        .HasColumnName("eval_observacion")
                        .HasMaxLength(500)
                        .IsUnicode(false);

                    b.Property<int>("PreregId")
                        .HasColumnName("prereg_id");

                    b.Property<int>("UsrId")
                        .HasColumnName("usr_id");

                    b.HasKey("EvalId");

                    b.HasIndex("PreregId");

                    b.HasIndex("UsrId");

                    b.ToTable("EVALUACION");
                });

            modelBuilder.Entity("Modelo.Models.Preregistros", b =>
                {
                    b.Property<int>("PreregId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("prereg_id");

                    b.Property<int>("CvId")
                        .HasColumnName("cv_id");

                    b.Property<int?>("EstprId")
                        .HasColumnName("estpr_id");

                    b.Property<string>("PreregApellidos")
                        .IsRequired()
                        .HasColumnName("prereg_apellidos")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<string>("PreregAreaProfesional")
                        .IsRequired()
                        .HasColumnName("prereg_area_profesional")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<string>("PreregEmail")
                        .IsRequired()
                        .HasColumnName("prereg_email")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<DateTime>("PreregFechaCreacion")
                        .HasColumnName("prereg_fecha_creacion")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("PreregFechaModificacion")
                        .HasColumnName("prereg_fecha_modificacion")
                        .HasColumnType("datetime");

                    b.Property<string>("PreregIdentificacion")
                        .IsRequired()
                        .HasColumnName("prereg_identificacion")
                        .HasMaxLength(150)
                        .IsUnicode(false);

                    b.Property<string>("PreregNombres")
                        .IsRequired()
                        .HasColumnName("prereg_nombres")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<string>("PreregTematica")
                        .IsRequired()
                        .HasColumnName("prereg_tematica")
                        .HasMaxLength(250)
                        .IsUnicode(false);

                    b.Property<int>("TipoprId")
                        .HasColumnName("tipopr_id");

                    b.HasKey("PreregId");

                    b.HasIndex("CvId");

                    b.HasIndex("EstprId");

                    b.HasIndex("TipoprId");

                    b.ToTable("PREREGISTROS");
                });

            modelBuilder.Entity("Modelo.Models.Roles", b =>
                {
                    b.Property<int>("RolId")
                        .HasColumnName("rol_id");

                    b.Property<string>("RolNombre")
                        .IsRequired()
                        .HasColumnName("rol_nombre")
                        .HasColumnType("char(50)");

                    b.HasKey("RolId");

                    b.ToTable("ROLES");
                });

            modelBuilder.Entity("Modelo.Models.TipoPreregistro", b =>
                {
                    b.Property<int>("TipoprId")
                        .HasColumnName("tipopr_id");

                    b.Property<string>("TipoprNombre")
                        .IsRequired()
                        .HasColumnName("tipopr_nombre")
                        .HasColumnType("char(50)");

                    b.HasKey("TipoprId");

                    b.ToTable("TIPO_PREREGISTRO");
                });

            modelBuilder.Entity("Modelo.Models.Usuario", b =>
                {
                    b.Property<int>("UsrId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("usr_id");

                    b.Property<int>("RolId")
                        .HasColumnName("rol_id");

                    b.Property<bool>("UsrActivo")
                        .HasColumnName("usr_activo");

                    b.Property<string>("UsrNombre")
                        .IsRequired()
                        .HasColumnName("usr_nombre")
                        .HasMaxLength(50)
                        .IsUnicode(false);

                    b.Property<string>("UsrPassword")
                        .IsRequired()
                        .HasColumnName("usr_password")
                        .HasMaxLength(15)
                        .IsUnicode(false);

                    b.HasKey("UsrId");

                    b.HasIndex("RolId");

                    b.ToTable("USUARIO");
                });

            modelBuilder.Entity("Modelo.Models.Evaluacion", b =>
                {
                    b.HasOne("Modelo.Models.Preregistros", "Prereg")
                        .WithMany("Evaluacion")
                        .HasForeignKey("PreregId")
                        .HasConstraintName("FK_EVALUACION_PREREGISTROS");

                    b.HasOne("Modelo.Models.Usuario", "Usr")
                        .WithMany("Evaluacion")
                        .HasForeignKey("UsrId")
                        .HasConstraintName("FK_EVALUACION_USUARIO");
                });

            modelBuilder.Entity("Modelo.Models.Preregistros", b =>
                {
                    b.HasOne("Modelo.Models.Cv", "Cv")
                        .WithMany("Preregistros")
                        .HasForeignKey("CvId")
                        .HasConstraintName("FK_PREREGISTROS_CV");

                    b.HasOne("Modelo.Models.EstadoPrereg", "Estpr")
                        .WithMany("Preregistros")
                        .HasForeignKey("EstprId")
                        .HasConstraintName("FK_PREREGISTROS_ESTADO_PREREG");

                    b.HasOne("Modelo.Models.TipoPreregistro", "Tipopr")
                        .WithMany("Preregistros")
                        .HasForeignKey("TipoprId")
                        .HasConstraintName("FK_PREREGISTROS_TIPO_PREREGISTRO");
                });

            modelBuilder.Entity("Modelo.Models.Usuario", b =>
                {
                    b.HasOne("Modelo.Models.Roles", "Rol")
                        .WithMany("Usuario")
                        .HasForeignKey("RolId")
                        .HasConstraintName("FK_USUARIO_ROLES");
                });
#pragma warning restore 612, 618
        }
    }
}