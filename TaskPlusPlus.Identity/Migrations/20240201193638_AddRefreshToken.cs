using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskPlusPlus.Identity.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                schema: "identity",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                schema: "identity",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp" },
                values: new object[] { "fd3c84ee-d822-4f08-b82c-4af8ca8599fc", "AQAAAAIAAYagAAAAEN+DClaskHtYryl5YslOa+FJKEcezQYEfeeWUtO/YbcSXkugrfPnb4HAi2hEJ1oF+w==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "473bcd98-26e5-4628-b084-4d35abb62420" });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiryTime", "SecurityStamp" },
                values: new object[] { "ade01433-5f9f-4e60-ad13-19c246576148", "AQAAAAIAAYagAAAAEFq/Dz4TSWNYKjm5FgUBK8AKquBfsyFUGpLbpSfK/X9Lj3zpxzbsLrDCFrCMLfwZDA==", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "78b7bf2c-cf17-46a9-a5b5-0932531e27b2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                schema: "identity",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "8e445865-a24d-4543-a6c6-9443d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d4f68891-5333-4d2e-97c9-818e48b9f2c1", "AQAAAAIAAYagAAAAEJud8Qln8LYzf7H5LLVL0OaxRwdtT+NOPo4uP30LuAt/yd2qVXGuyZIIzV6IfkFB0Q==", "39ff9ffe-cdf4-4537-9ecf-c98d6ef9e792" });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1d6142b-8778-432e-a08f-cd449881e1d8", "AQAAAAIAAYagAAAAEBFP/aNllPWP2yhSIzH8CCWPZHHmolFkD7IT5Iuh9el9C9iSJ4z7HUPAS4gvIx48QQ==", "a7ad3e3e-60ea-4268-9a61-b3bc5489c3eb" });
        }
    }
}
