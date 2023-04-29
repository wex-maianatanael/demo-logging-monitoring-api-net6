using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Infra.Repository.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    BankID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Accounts_Banks_BankID",
                        column: x => x.BankID,
                        principalTable: "Banks",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Accounts_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "ID", "Name" },
                values: new object[] { new Guid("aba81c6a-8983-4e30-8d51-9d9ea790a292"), "Bank 1" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "ID", "BirthDate", "Name" },
                values: new object[,]
                {
                    { new Guid("4203b06b-f962-4e59-b848-a43a2cceb6f4"), new DateTime(2000, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bob" },
                    { new Guid("a355cea6-ecb7-491e-857e-e115fc57ff95"), new DateTime(1998, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice" },
                    { new Guid("f9748ff6-195f-4279-830b-1d1576fbf87c"), new DateTime(1986, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Kyle" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "ID", "Active", "Balance", "BankID", "CreatedDate", "CustomerID", "Number" },
                values: new object[] { new Guid("47a864d4-04de-444b-9ff5-fa6e4cbb73cd"), true, 2300m, new Guid("aba81c6a-8983-4e30-8d51-9d9ea790a292"), new DateTime(2023, 4, 29, 19, 25, 10, 408, DateTimeKind.Local).AddTicks(8620), new Guid("4203b06b-f962-4e59-b848-a43a2cceb6f4"), 333333 });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "ID", "Active", "Balance", "BankID", "CreatedDate", "CustomerID", "Number" },
                values: new object[] { new Guid("4be52ed1-d294-49f8-8423-710c5d66be79"), true, 1200m, new Guid("aba81c6a-8983-4e30-8d51-9d9ea790a292"), new DateTime(2023, 4, 29, 19, 25, 10, 408, DateTimeKind.Local).AddTicks(8596), new Guid("a355cea6-ecb7-491e-857e-e115fc57ff95"), 222222 });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "ID", "Active", "Balance", "BankID", "CreatedDate", "CustomerID", "Number" },
                values: new object[] { new Guid("fc3b2cfc-e334-4b3b-9cf9-65bcfdc31c17"), true, 1000m, new Guid("aba81c6a-8983-4e30-8d51-9d9ea790a292"), new DateTime(2023, 4, 29, 19, 25, 10, 408, DateTimeKind.Local).AddTicks(8574), new Guid("f9748ff6-195f-4279-830b-1d1576fbf87c"), 111111 });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_BankID",
                table: "Accounts",
                column: "BankID");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CustomerID",
                table: "Accounts",
                column: "CustomerID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
