using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class AddProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 10, 23, 10, 51, 23, 883, DateTimeKind.Local).AddTicks(6950));

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(maxLength: 200, nullable: false),
                    Caption = table.Column<string>(maxLength: 200, nullable: true),
                    IsDefault = table.Column<bool>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    SortOrder = table.Column<int>(nullable: false),
                    FileSize = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("57b2f055-230b-49bd-be3f-58440e0ecfa7"),
                column: "ConcurrencyStamp",
                value: "2ed9fb02-702f-4410-a053-6e4cd5dc345e");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("07bda57c-0a84-44b4-855a-48748863b649"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "adaf0a12-4e0b-47a8-bd1e-2e7caac2f12f", "AQAAAAEAACcQAAAAEPVhccVtNtr8mFBD6ruet5oKIfrJLLfhZ2QUxtAaHOnF5MWKzvPywCi+YKU60M7Fng==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 10, 24, 22, 50, 10, 339, DateTimeKind.Local).AddTicks(6848));

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 10, 23, 10, 51, 23, 883, DateTimeKind.Local).AddTicks(6950),
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("57b2f055-230b-49bd-be3f-58440e0ecfa7"),
                column: "ConcurrencyStamp",
                value: "9e53cebc-c12c-463c-803f-147f6805cab9");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("07bda57c-0a84-44b4-855a-48748863b649"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c165566c-b2b7-43fe-9c16-4ce33c1f9363", "AQAAAAEAACcQAAAAEDJtp3j01kj5VqJSDGkiUuoh4J5q/+SgciFMz+yJDtC/4X9iVNTrFefoBdR20+jFXQ==" });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Status",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2025, 10, 23, 10, 51, 23, 894, DateTimeKind.Local).AddTicks(8269));
        }
    }
}
