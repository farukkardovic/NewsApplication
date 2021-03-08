using Microsoft.EntityFrameworkCore.Migrations;
using System.Text;

namespace SoftNews.Repository.Migrations
{
    public partial class AddSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            byte[] passwordHash, passwordSalt;

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes("faruk"));
            }

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Username", "FirstName", "LastName", "PasswordHash", "PasswordSalt" },
                values: new object[] { "faruk", "Faruk", "Kardovic", passwordHash, passwordSalt });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Name" },
                values: new object[] { "Crna hronika" }
                );

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Name" },
                values: new object[] { "Svijet" }
                );
            migrationBuilder.InsertData(
               table: "Categories",
               columns: new[] { "Name" },
               values: new object[] { "Kultura" }
               );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
