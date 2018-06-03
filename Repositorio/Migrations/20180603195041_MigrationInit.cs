using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Repositorio.Migrations
{
    public partial class MigrationInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CV",
                columns: table => new
                {
                    cv_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cv_adjunto = table.Column<byte[]>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CV", x => x.cv_id);
                });

            migrationBuilder.CreateTable(
                name: "ESTADO_PREREG",
                columns: table => new
                {
                    estpr_id = table.Column<int>(nullable: false),
                    estpr_nombre = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ESTADO_PREREG", x => x.estpr_id);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                columns: table => new
                {
                    rol_id = table.Column<int>(nullable: false),
                    rol_nombre = table.Column<string>(type: "char(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.rol_id);
                });

            migrationBuilder.CreateTable(
                name: "TIPO_PREREGISTRO",
                columns: table => new
                {
                    tipopr_id = table.Column<int>(nullable: false),
                    tipopr_nombre = table.Column<string>(type: "char(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPO_PREREGISTRO", x => x.tipopr_id);
                });

            migrationBuilder.CreateTable(
                name: "USUARIO",
                columns: table => new
                {
                    usr_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    rol_id = table.Column<int>(nullable: false),
                    usr_activo = table.Column<bool>(nullable: false),
                    usr_nombre = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    usr_password = table.Column<string>(unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USUARIO", x => x.usr_id);
                    table.ForeignKey(
                        name: "FK_USUARIO_ROLES",
                        column: x => x.rol_id,
                        principalTable: "ROLES",
                        principalColumn: "rol_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PREREGISTROS",
                columns: table => new
                {
                    prereg_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    cv_id = table.Column<int>(nullable: false),
                    estpr_id = table.Column<int>(nullable: true),
                    prereg_apellidos = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    prereg_area_profesional = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    prereg_email = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    prereg_fecha_creacion = table.Column<DateTime>(type: "datetime", nullable: false),
                    prereg_fecha_modificacion = table.Column<DateTime>(type: "datetime", nullable: true),
                    prereg_identificacion = table.Column<string>(unicode: false, maxLength: 150, nullable: false),
                    prereg_nombres = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    prereg_tematica = table.Column<string>(unicode: false, maxLength: 250, nullable: false),
                    tipopr_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PREREGISTROS", x => x.prereg_id);
                    table.ForeignKey(
                        name: "FK_PREREGISTROS_CV",
                        column: x => x.cv_id,
                        principalTable: "CV",
                        principalColumn: "cv_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PREREGISTROS_ESTADO_PREREG",
                        column: x => x.estpr_id,
                        principalTable: "ESTADO_PREREG",
                        principalColumn: "estpr_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PREREGISTROS_TIPO_PREREGISTRO",
                        column: x => x.tipopr_id,
                        principalTable: "TIPO_PREREGISTRO",
                        principalColumn: "tipopr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EVALUACION",
                columns: table => new
                {
                    eval_id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    eval_observacion = table.Column<string>(unicode: false, maxLength: 500, nullable: false),
                    prereg_id = table.Column<int>(nullable: false),
                    usr_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EVALUACION", x => x.eval_id);
                    table.ForeignKey(
                        name: "FK_EVALUACION_PREREGISTROS",
                        column: x => x.prereg_id,
                        principalTable: "PREREGISTROS",
                        principalColumn: "prereg_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EVALUACION_USUARIO",
                        column: x => x.usr_id,
                        principalTable: "USUARIO",
                        principalColumn: "usr_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EVALUACION_prereg_id",
                table: "EVALUACION",
                column: "prereg_id");

            migrationBuilder.CreateIndex(
                name: "IX_EVALUACION_usr_id",
                table: "EVALUACION",
                column: "usr_id");

            migrationBuilder.CreateIndex(
                name: "IX_PREREGISTROS_cv_id",
                table: "PREREGISTROS",
                column: "cv_id");

            migrationBuilder.CreateIndex(
                name: "IX_PREREGISTROS_estpr_id",
                table: "PREREGISTROS",
                column: "estpr_id");

            migrationBuilder.CreateIndex(
                name: "IX_PREREGISTROS_tipopr_id",
                table: "PREREGISTROS",
                column: "tipopr_id");

            migrationBuilder.CreateIndex(
                name: "IX_USUARIO_rol_id",
                table: "USUARIO",
                column: "rol_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EVALUACION");

            migrationBuilder.DropTable(
                name: "PREREGISTROS");

            migrationBuilder.DropTable(
                name: "USUARIO");

            migrationBuilder.DropTable(
                name: "CV");

            migrationBuilder.DropTable(
                name: "ESTADO_PREREG");

            migrationBuilder.DropTable(
                name: "TIPO_PREREGISTRO");

            migrationBuilder.DropTable(
                name: "ROLES");
        }
    }
}
