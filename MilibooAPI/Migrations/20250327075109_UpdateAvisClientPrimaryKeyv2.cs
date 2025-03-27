using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MilibooAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAvisClientPrimaryKeyv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "uqix_avisclient_titreavis",
                schema: "miliboo",
                table: "t_e_avis_client_avc");

            migrationBuilder.AlterColumn<DateTime>(
                name: "avc_dateavis",
                schema: "miliboo",
                table: "t_e_avis_client_avc",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "NOW()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "now()");

            migrationBuilder.AlterColumn<int>(
                name: "avc_idavis",
                schema: "miliboo",
                table: "t_e_avis_client_avc",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "avc_dateavis",
                schema: "miliboo",
                table: "t_e_avis_client_avc",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "NOW()");

            migrationBuilder.AlterColumn<int>(
                name: "avc_idavis",
                schema: "miliboo",
                table: "t_e_avis_client_avc",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.CreateIndex(
                name: "uqix_avisclient_titreavis",
                schema: "miliboo",
                table: "t_e_avis_client_avc",
                column: "avc_titreavis");
        }
    }
}
