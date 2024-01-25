using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace proyecto_api.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaProyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Proyectos",
                columns: new[] { "Id", "Autor", "Descripcion", "FechaPublicacion", "Nombre", "Puntuacion", "Terminado" },
                values: new object[,]
                {
                    { 1, "Mimosa Misas", "Ninguna...", new DateTime(2023, 12, 27, 16, 26, 34, 450, DateTimeKind.Local).AddTicks(6482), "La niña del aro", 2.0, false },
                    { 2, "Juan Misas", "pelicula fenizi...", new DateTime(2023, 12, 27, 16, 26, 34, 450, DateTimeKind.Local).AddTicks(6494), "Barbie", 2.0, false }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Proyectos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Proyectos",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
