using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CareJobApplicationv2.Data.Migrations
{
    public partial class SeeRoll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id","Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), "Evaluator", "Evaluator".ToUpper(), Guid.NewGuid().ToString() }
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELET FROM [AspNetRoles]");
        }
    }
}
