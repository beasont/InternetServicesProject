using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ResetIdentitySeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DBCC CHECKIDENT('Categories', RESEED, 10);");
            migrationBuilder.Sql("DBCC CHECKIDENT('Products',   RESEED, 10);");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
