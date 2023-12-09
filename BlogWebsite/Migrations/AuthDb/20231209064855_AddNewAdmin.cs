using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BlogWebsite.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class AddNewAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fb79341-d865-49cd-a8fa-08cd397dd408",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "38d50c91-f950-4d52-a854-2b4e9a9349dd", "AQAAAAIAAYagAAAAEOd6TpZYNevHNAKAhanvP3v19iVYGD6vBi0F1BK3TvmcW31ll8kph2QLEol8AhIZ9A==", "764557a1-f23f-4c94-9457-2fddf41dc95a" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "51ae3cfb-5e28-4e9b-b7aa-8a655b449756", 0, "82ad2dcd-875d-4b87-8815-757943608d81", "newadmin@bloggie.com", false, false, null, "NEWADMIN@BLOGGIE.COM", "NEWADMIN@BLOGGIE.COM", "AQAAAAIAAYagAAAAELrl7m3NqKOXFnEhrfgiwM87fmYi56PTRk3XCHm1s7W/Z3ptoEIzyRvxmqlzXlMXMw==", null, false, "dc788690-953c-450c-9e4a-8f31e5142907", false, "newadmin@bloggie.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "374cfae7-9e47-4a0c-aa04-1be9e5a1ce8d", "51ae3cfb-5e28-4e9b-b7aa-8a655b449756" },
                    { "43e56eed-6303-4310-a8ee-a8a49f48f568", "51ae3cfb-5e28-4e9b-b7aa-8a655b449756" },
                    { "847782ad-52b1-462c-9cbd-8180c3e3c8c6", "51ae3cfb-5e28-4e9b-b7aa-8a655b449756" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "374cfae7-9e47-4a0c-aa04-1be9e5a1ce8d", "51ae3cfb-5e28-4e9b-b7aa-8a655b449756" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "43e56eed-6303-4310-a8ee-a8a49f48f568", "51ae3cfb-5e28-4e9b-b7aa-8a655b449756" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "847782ad-52b1-462c-9cbd-8180c3e3c8c6", "51ae3cfb-5e28-4e9b-b7aa-8a655b449756" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51ae3cfb-5e28-4e9b-b7aa-8a655b449756");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fb79341-d865-49cd-a8fa-08cd397dd408",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f322451-ac13-4f56-9703-8141584aae58", "AQAAAAIAAYagAAAAECX+b6OZPTbhyRzpHeZe8eIqcXdyUjToqnFuFca9InhDV1LBVQocr+wOYDgs4kze8w==", "0e9690ba-a966-409d-b447-b87856e24a57" });
        }
    }
}
