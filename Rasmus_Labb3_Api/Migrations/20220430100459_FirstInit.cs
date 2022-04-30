using Microsoft.EntityFrameworkCore.Migrations;

namespace Rasmus_labb3_API.Migrations
{
    public partial class FirstInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interests",
                columns: table => new
                {
                    InterestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interests", x => x.InterestId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    PhoneNr = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    LinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkUrl = table.Column<string>(nullable: false),
                    InterestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.LinkId);
                    table.ForeignKey(
                        name: "FK_Links_Interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interests",
                        principalColumn: "InterestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonInterests",
                columns: table => new
                {
                    PersonInterestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    InterestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonInterests", x => x.PersonInterestId);
                    table.ForeignKey(
                        name: "FK_PersonInterests_Interests_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interests",
                        principalColumn: "InterestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonInterests_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Interests",
                columns: new[] { "InterestId", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Amazing world down there.", "Scuba Diving" },
                    { 2, "Compete against yourself!", "Golf" },
                    { 3, "The earth is your backyard.", "Hiking" },
                    { 4, "Gotta score em' goals.", "Football" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "Name", "PhoneNr" },
                values: new object[,]
                {
                    { 1, "Rasmus Ekbom", 706899532 },
                    { 2, "Anitra Ngoensuwan", 701234567 },
                    { 3, "Fredrik Olsson", 709876645 }
                });

            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "LinkId", "InterestId", "LinkUrl" },
                values: new object[,]
                {
                    { 1, 1, "www.scubadiving.se" },
                    { 2, 2, "www.golf.se" },
                    { 3, 3, "www.hiking.se" },
                    { 4, 4, "www.football.se" }
                });

            migrationBuilder.InsertData(
                table: "PersonInterests",
                columns: new[] { "PersonInterestId", "InterestId", "PersonId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 6, 3, 1 },
                    { 2, 2, 2 },
                    { 5, 1, 2 },
                    { 3, 3, 3 },
                    { 4, 1, 3 },
                    { 7, 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_InterestId",
                table: "Links",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInterests_InterestId",
                table: "PersonInterests",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonInterests_PersonId",
                table: "PersonInterests",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "PersonInterests");

            migrationBuilder.DropTable(
                name: "Interests");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
