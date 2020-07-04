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
                    name = table.Column<string>(type: "varchar(150)", nullable: false),
                    description = table.Column<string>(type: "varchar(250)", nullable: true)
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
                    name = table.Column<string>(type: "varchar(150)", nullable: false),
                    description = table.Column<string>(type: "varchar(250)", nullable: true),
                    company_id = table.Column<long>(nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: true)
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
                    review_date = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    stars = table.Column<int>(type: "int", nullable: false),
                    title = table.Column<string>(type: "varchar(150)", nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "text_analysis",
                schema: "review_application",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    review_id = table.Column<long>(nullable: false),
                    query_date = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    json = table.Column<string>(type: "text", nullable: true),
                    sentiment = table.Column<string>(type: "varchar(50)", nullable: true),
                    positive_score = table.Column<double>(nullable: false),
                    neutral_score = table.Column<double>(nullable: false),
                    negative_score = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_text_analysis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_text_analysis_review_review_id",
                        column: x => x.review_id,
                        principalSchema: "review_application",
                        principalTable: "review",
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

            migrationBuilder.CreateIndex(
                name: "idx_review_starts",
                schema: "review_application",
                table: "review",
                column: "stars");

            migrationBuilder.CreateIndex(
                name: "idx_review_title",
                schema: "review_application",
                table: "review",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "idx_text_analysis_query_date",
                schema: "review_application",
                table: "text_analysis",
                column: "query_date");

            migrationBuilder.CreateIndex(
                name: "IX_text_analysis_review_id",
                schema: "review_application",
                table: "text_analysis",
                column: "review_id");

            migrationBuilder.CreateIndex(
                name: "idx_text_analysis_sentiment",
                schema: "review_application",
                table: "text_analysis",
                column: "sentiment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "text_analysis",
                schema: "review_application");

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
