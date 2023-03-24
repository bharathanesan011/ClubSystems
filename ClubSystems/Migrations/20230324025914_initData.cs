using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClubSystems.Migrations
{
    public partial class initData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "AccountBalance",
                table: "MemberShip",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.InsertData(
                table: "Person",
                columns: new[] { "PersonID", "Email", "Forenames", "Phone", "PostCode", "Surname" },
                values: new object[] { 1, "johnc@gmail.com", "John", "7894562310", "IG11PT", "Cooper" });

            migrationBuilder.InsertData(
                table: "MemberShip",
                columns: new[] { "MemberShipNumber", "AccountBalance", "IsOverdrawn", "MemberShipType", "PersonID" },
                values: new object[] { 1, 10000.34, false, 0, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MemberShip",
                keyColumn: "MemberShipNumber",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Person",
                keyColumn: "PersonID",
                keyValue: 1);

            migrationBuilder.AlterColumn<decimal>(
                name: "AccountBalance",
                table: "MemberShip",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }
    }
}
