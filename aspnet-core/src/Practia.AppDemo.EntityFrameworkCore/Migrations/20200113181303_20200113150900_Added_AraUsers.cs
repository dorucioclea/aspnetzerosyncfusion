using Microsoft.EntityFrameworkCore.Migrations;

namespace Practia.AppDemo.Migrations
{
    public partial class _20200113150900_Added_AraUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "arausers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: true),
                    user_id = table.Column<int>(nullable: false),
                    user_name = table.Column<string>(nullable: false),
                    user_real_name = table.Column<string>(nullable: true),
                    user_email = table.Column<string>(nullable: true),
                    prof_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arausers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_arausers_araprofiles_prof_id",
                        column: x => x.prof_id,
                        principalTable: "araprofiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_arausers_TenantId",
                table: "arausers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_arausers_prof_id",
                table: "arausers",
                column: "prof_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "arausers");
        }
    }
}
