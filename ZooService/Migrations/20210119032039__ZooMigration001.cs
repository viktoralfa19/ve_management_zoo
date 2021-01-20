using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ZooService.Migrations
{
    public partial class _ZooMigration001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnimalType",
                columns: table => new
                {
                    Id = table.Column<short>(nullable: false, comment: "Primary key animal type table")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(maxLength: 16, nullable: false, comment: "Code for the animal register."),
                    Name = table.Column<string>(maxLength: 32, nullable: false, comment: "Code for the animal register.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false, comment: "Primary key animal table")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id_AnimalType = table.Column<short>(nullable: false, comment: "Foreign Key for table animal type."),
                    Code = table.Column<string>(maxLength: 16, nullable: false, comment: "Code for the animal register."),
                    Name = table.Column<string>(maxLength: 64, nullable: false, comment: "Code for the animal register."),
                    DateAdmission = table.Column<DateTime>(type: "timestamp", nullable: false, comment: "Date for the register was entered the zoo."),
                    Weight = table.Column<decimal>(type: "numeric(8,2)", nullable: false, comment: "Weight for the animal register."),
                    DateCreated = table.Column<DateTime>(type: "timestamp", nullable: false, comment: "Date for the register was created in database."),
                    DateUpdated = table.Column<DateTime>(type: "timestamp", nullable: true, comment: "Date for the register was updated in database."),
                    UserCreated = table.Column<Guid>(type: "uuid", nullable: false, comment: "User that to register in database."),
                    UserUpdated = table.Column<Guid>(type: "uuid", nullable: true, comment: "User that to update in database."),
                    IdSuscripcion = table.Column<short>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ANIMAL_ANIMAL_TYPE",
                        column: x => x.Id_AnimalType,
                        principalTable: "AnimalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Store data information about animals");

            migrationBuilder.InsertData(
                table: "AnimalType",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { (short)1, "LN", "León" },
                    { (short)2, "TG", "Tigre" },
                    { (short)3, "ELF", "Elefante" }
                });

            migrationBuilder.CreateIndex(
                name: "ANIMAL_ANIMAL_TYPE_FK",
                table: "Animal",
                column: "Id_AnimalType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "AnimalType");
        }
    }
}
