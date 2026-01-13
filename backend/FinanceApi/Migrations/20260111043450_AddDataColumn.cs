using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FinanceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddDataColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_gastos",
                table: "gastos");

            migrationBuilder.RenameTable(
                name: "gastos",
                newName: "Gastos");

            migrationBuilder.RenameColumn(
                name: "valor",
                table: "Gastos",
                newName: "Valor");

            migrationBuilder.RenameColumn(
                name: "descricao",
                table: "Gastos",
                newName: "Descricao");

            migrationBuilder.RenameColumn(
                name: "categoria",
                table: "Gastos",
                newName: "Categoria");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Gastos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "Gastos",
                newName: "Data");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valor",
                table: "Gastos",
                type: "numeric(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AlterColumn<string>(
                name: "Categoria",
                table: "Gastos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gastos",
                table: "Gastos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gastos",
                table: "Gastos");

            migrationBuilder.RenameTable(
                name: "Gastos",
                newName: "gastos");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "gastos",
                newName: "valor");

            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "gastos",
                newName: "descricao");

            migrationBuilder.RenameColumn(
                name: "Categoria",
                table: "gastos",
                newName: "categoria");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "gastos",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "gastos",
                newName: "data_criacao");

            migrationBuilder.AlterColumn<decimal>(
                name: "valor",
                table: "gastos",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "categoria",
                table: "gastos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_gastos",
                table: "gastos",
                column: "id");
        }
    }
}
