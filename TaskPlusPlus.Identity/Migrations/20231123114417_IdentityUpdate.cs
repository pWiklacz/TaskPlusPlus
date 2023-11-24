using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskPlusPlus.Identity.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e87c55f-6542-4b4c-9780-5237919a6939", "AQAAAAIAAYagAAAAEOHVp5aQaN+MKlyAAcbV/TBpk41Rrn4DSO4cJ264cORZzs1+18p1J5BxMdoRHfdONg==", "7fc3d7d0-4d23-44bd-a3ca-289688233de0" });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "513249f4-90b0-4d21-8099-618c51ba1a61", "AQAAAAIAAYagAAAAEN0xTJXzIrWAvk4bThUvXTgK+5z5XNho3XuxWBLWtroC+g456vIoxw+yKE9F0DGBtw==", "95df371b-c7b3-4025-becd-1ddccaa36efc" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "849cb110-c2b1-4976-b842-7d0a28a84430", "AQAAAAIAAYagAAAAEGtKEx6GijsXaAmtu0wki5nBCliPEH4+yAJe+J0btDzyFO/xGkYmcDuX27TOoYXvVA==", "0fd32a67-e7bf-4013-b69e-45bd73f4a284" });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0b6ce28d-103c-4127-bf91-6592c8eafee4", "AQAAAAIAAYagAAAAEFC6mAEaXM3wMd18Hy5LeziLBp12nMJ3vfzgWci1GolA6LX1OMN8vPgD7wpsXCc7GQ==", "01370318-de62-436a-b929-22603b7dd875" });
        }
    }
}
