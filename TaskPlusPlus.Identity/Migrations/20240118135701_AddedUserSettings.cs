using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskPlusPlus.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Settings",
                schema: "identity",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "933e0ca4-b0e9-4597-a28e-b9608277f30a", "AQAAAAIAAYagAAAAEC1XcNES1orL0n+bHq+IT6Hzro+ah33/j+TMvl5DLIk7Gv7rZjDbBsWnMD39TpjYjA==", "912ea212-d532-4f37-a64e-0dc5b6eb90e9" });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c92064d-406d-4164-b9e7-4b7bcb569a02", "AQAAAAIAAYagAAAAED1ptJ6zXYPtiB+OCev9C4+Qz9xu8PAyDlsa0Mdo8OMl2TlDU2mNjl31Tqqp/zbJdA==", "76c927d6-be57-4c7c-9bba-b8ebf42d80b2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Settings",
                schema: "identity",
                table: "AspNetUsers");

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
    }
}
