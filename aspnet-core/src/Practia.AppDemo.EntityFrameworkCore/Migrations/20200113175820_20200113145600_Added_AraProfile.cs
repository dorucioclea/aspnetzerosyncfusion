using Microsoft.EntityFrameworkCore.Migrations;

namespace Practia.AppDemo.Migrations
{
    public partial class _20200113145600_Added_AraProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "araprofiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: true),
                    prof_id = table.Column<int>(nullable: false),
                    prof_description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_araprofiles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_araprofiles_TenantId",
                table: "araprofiles",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "araprofiles");
        }
    }
}
