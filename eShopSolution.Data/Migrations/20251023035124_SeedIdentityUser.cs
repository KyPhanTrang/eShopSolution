using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.Data.Migrations
{
    public partial class SeedIdentityUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2025, 10, 23, 10, 51, 23, 883, DateTimeKind.Local).AddTicks(6950),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2025, 10, 23, 0, 19, 33, 516, DateTimeKind.Local).AddTicks(4605));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("57b2f055-230b-49bd-be3f-58440e0ecfa7"), "9e53cebc-c12c-463c-803f-147f6805cab9", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("07bda57c-0a84-44b4-855a-48748863b649"), new Guid("57b2f055-230b-49bd-be3f-58440e0ecfa7") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Dob", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("07bda57c-0a84-44b4-855a-48748863b649"), 0, "c165566c-b2b7-43fe-9c16-4ce33c1f9363", new DateTime(2005, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "truonghuyphu07@gmail.com", true, "Phu", "Truong Huy", false, null, "truonghuyphu07@gmail.com", "admin", "AQAAAAEAACcQAAAAEDJtp3j01kj5VqJSDGkiUuoh4J5q/+SgciFMz+yJDtC/4X9iVNTrFefoBdR20+jFXQ==", null, false, "", false, "admin" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("57b2f055-230b-49bd-be3f-58440e0ecfa7"));

            migrationBuilder.DeleteData(
                table: "AppUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { new Guid("07bda57c-0a84-44b4-855a-48748863b649"), new Guid("57b2f055-230b-49bd-be3f-58440e0ecfa7") });

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("07bda57c-0a84-44b4-855a-48748863b649"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2025, 10, 23, 0, 19, 33, 516, DateTimeKind.Local).AddTicks(4605),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2025, 10, 23, 10, 51, 23, 883, DateTimeKind.Local).AddTicks(6950));

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
                value: new DateTime(2025, 10, 23, 0, 19, 33, 528, DateTimeKind.Local).AddTicks(2241));
        }
    }
}
