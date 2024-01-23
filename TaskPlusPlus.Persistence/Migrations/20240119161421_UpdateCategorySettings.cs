using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskPlusPlus.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategorySettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE application.Categories SET Settings = '{\"Direction\":false,\"Grouping\":\"None\",\"Sorting\":\"Name\"}';");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
