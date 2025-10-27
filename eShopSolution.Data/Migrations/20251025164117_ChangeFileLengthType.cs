using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class ChangeFileLengthType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FileSize",
                table: "ProductImages",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("57b2f055-230b-49bd-be3f-58440e0ecfa7"),
                column: "ConcurrencyStamp",
                value: "65ae7b83-aaa5-4e8f-b369-26b68aa53fe7");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("07bda57c-0a84-44b4-855a-48748863b649"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8127a92f-92d9-4357-942b-88fb6f6ebce0", "AQAAAAEAACcQAAAAEIlwtNz+t52Y6z/uzQG89gyxZnA2tX7z4FrxkYd/RIB5+9tqhAWePWiHK6JM4a9ygg==" });

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
                value: new DateTime(2025, 10, 25, 23, 41, 15, 953, DateTimeKind.Local).AddTicks(6902));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "FileSize",
                table: "ProductImages",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

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
        }
    }
}
