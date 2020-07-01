using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace ReviewApp.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "review_application");

            migrationBuilder.CreateTable(
                name: "company",
                schema: "review_application",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                schema: "review_application",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    company_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_company_company_id",
                        column: x => x.company_id,
                        principalSchema: "review_application",
                        principalTable: "company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "review",
                schema: "review_application",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    review_date = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    content = table.Column<string>(nullable: false),
                    product_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_review_product_product_id",
                        column: x => x.product_id,
                        principalSchema: "review_application",
                        principalTable: "product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "uq_company_name",
                schema: "review_application",
                table: "company",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_product_company_id",
                schema: "review_application",
                table: "product",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "uq_product_name",
                schema: "review_application",
                table: "product",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_review_product_id",
                schema: "review_application",
                table: "review",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "idx_review_date",
                schema: "review_application",
                table: "review",
                column: "review_date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "review",
                schema: "review_application");

            migrationBuilder.DropTable(
                name: "product",
                schema: "review_application");

            migrationBuilder.DropTable(
                name: "company",
                schema: "review_application");
        }
    }
}
