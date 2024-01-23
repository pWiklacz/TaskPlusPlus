using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskPlusPlus.Identity.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserSettings : Migration
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
                values: new object[] { "d4f68891-5333-4d2e-97c9-818e48b9f2c1", "AQAAAAIAAYagAAAAEJud8Qln8LYzf7H5LLVL0OaxRwdtT+NOPo4uP30LuAt/yd2qVXGuyZIIzV6IfkFB0Q==", "39ff9ffe-cdf4-4537-9ecf-c98d6ef9e792" });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c1d6142b-8778-432e-a08f-cd449881e1d8", "AQAAAAIAAYagAAAAEBFP/aNllPWP2yhSIzH8CCWPZHHmolFkD7IT5Iuh9el9C9iSJ4z7HUPAS4gvIx48QQ==", "a7ad3e3e-60ea-4268-9a61-b3bc5489c3eb" });

            migrationBuilder.Sql(
                "UPDATE [identity].[AspNetUsers] SET Settings = '{\"DateFormat\":\"DD-MM-YYYY\",\"Language\":\"ENG\",\"StartPage\":\"Inbox\",\"Theme\":\"pulse\",\"TimeFormat\":\"12\",\"InboxSettings\":{\"Direction\":false,\"Grouping\":\"None\",\"Sorting\":\"Name\"},\"NextActionsSettings\":{\"Direction\":false,\"Grouping\":\"None\",\"Sorting\":\"Name\"},\"SomedaySettings\":{\"Direction\":false,\"Grouping\":\"None\",\"Sorting\":\"Name\"},\"TodaySettings\":{\"Direction\":false,\"Grouping\":\"None\",\"Sorting\":\"Name\"},\"WaitingForSettings\":{\"Direction\":false,\"Grouping\":\"None\",\"Sorting\":\"Name\"}}';");

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
                values: new object[] { "933e0ca4-b0e9-4597-a28e-b9608277f30a", "AQAAAAIAAYagAAAAEC1XcNES1orL0n+bHq+IT6Hzro+ah33/j+TMvl5DLIk7Gv7rZjDbBsWnMD39TpjYjA==", "912ea212-d532-4f37-a64e-0dc5b6eb90e9" });

            migrationBuilder.UpdateData(
                schema: "identity",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e224968-33e4-4652-b7b7-8574d048cdb9",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7c92064d-406d-4164-b9e7-4b7bcb569a02", "AQAAAAIAAYagAAAAED1ptJ6zXYPtiB+OCev9C4+Qz9xu8PAyDlsa0Mdo8OMl2TlDU2mNjl31Tqqp/zbJdA==", "76c927d6-be57-4c7c-9bba-b8ebf42d80b2" });
        }
    }
}
