using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS_Eind.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Landlord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Age = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Landlord", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rooms = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LandlordId = table.Column<int>(type: "int", nullable: false),
                    SubTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePerDay = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    LocationFeatures = table.Column<int>(type: "int", nullable: false),
                    NumberOfGuests = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Landlord_LandlordId",
                        column: x => x.LandlordId,
                        principalTable: "Landlord",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCover = table.Column<bool>(type: "bit", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    LandlordId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Landlord_LandlordId",
                        column: x => x.LandlordId,
                        principalTable: "Landlord",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Image_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<float>(type: "real", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservation_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customer",
                columns: new[] { "Id", "Email", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "alice.smith@example.com", "Alice", "Smith" },
                    { 2, "bob.jones@example.com", "Bob", "Jones" },
                    { 3, "alice.Wonder@example.com", "Alice", "InWonderLand" },
                    { 4, "bob.Smiles@example.com", "Tim", "Smiles" }
                });

            migrationBuilder.InsertData(
                table: "Landlord",
                columns: new[] { "Id", "Age", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, 40, "John", "Doe" },
                    { 2, 55, "Mister", "Bean" },
                    { 3, 40, "Johny", "Test" },
                    { 4, 35, "Mister", "Mister" }
                });

            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "Id", "IsCover", "LandlordId", "LocationId", "Url" },
                values: new object[,]
                {
                    { 8, true, 1, null, "https://tse4.mm.bing.net/th?id=OIP.iBoBzz9cJd-jmN7jBpM2HwHaKB&pid=Api" },
                    { 9, true, 2, null, "https://tse4.mm.bing.net/th?id=OIP.bkfMC2AL9D8fFqfyAmsTqAHaI7&pid=Api" },
                    { 10, true, 3, null, "https://tse4.mm.bing.net/th?id=OIP.q99MVI-6mP4jD1NpWGR8bAHaHa&pid=Api" },
                    { 11, true, 4, null, "https://tse4.mm.bing.net/th?id=OIP.MB0oW9c-Mr-w-wPv91_VsgHaLH&pid=Api" }
                });

            migrationBuilder.InsertData(
                table: "Location",
                columns: new[] { "Id", "Description", "LandlordId", "LocationFeatures", "NumberOfGuests", "PricePerDay", "Rooms", "SubTitle", "Title", "Type" },
                values: new object[,]
                {
                    { 1, "De camping ligt verscholen achter de boerderij in de polder. Op fietsafstand (5 minuten) liggen het dorpje Nieuwvliet, de zee, het strand, het bos van Erasmus en het natuurgebied de Knokkert.", 1, 0, 12, 300f, 5, "Lekker veel ruimte", "De Boerenhoeve", 0 },
                    { 2, "Nee, dit puike penthouse dat al jaren te koop stond en nu is verkocht, is niet de duurste woning van ons land. Bij lange na niet. Wel is de meer dan €30.000 per vierkante meter woonruimte een record in ons land.", 2, 0, 4, 400f, 2, "Te gek uitzicht", "Frankie's Penthouse", 2 },
                    { 3, "A cozy cottage near the lake with beautiful view", 4, 38, 4, 100f, 2, "Near the lake", "Cozy Cottage", 1 },
                    { 4, "A luxury apartment in the heart of the city", 3, 60, 6, 200f, 3, "Downtown", "Luxury Apartment", 0 }
                });

            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "Id", "IsCover", "LandlordId", "LocationId", "Url" },
                values: new object[,]
                {
                    { 1, true, null, 1, "http://cdn.home-designing.com/wp-content/uploads/2010/11/Gingerbread-cottage-house-beautiful-landscape.jpg" },
                    { 2, true, null, 2, "http://weknowyourdreams.com/images/penthouse/penthouse-01.jpg" },
                    { 3, true, null, 3, "http://www.cottageblog.ca/wp-content/uploads/LakeJoeCabin-1280.jpg" },
                    { 4, true, null, 4, "https://tse3.mm.bing.net/th?id=OIP.5Nd1LqWRLikaFVlmbw2RfQHaLG&pid=Api" },
                    { 5, false, null, 2, "https://tse1.explicit.bing.net/th?id=OIP.tOrydk5j46G7kWuS1elhsgHaE8&pid=Api" },
                    { 6, false, null, 3, "https://tse1.mm.bing.net/th?id=OIP.KXS2egJ9qayUAMdLc9cCYQHaFX&pid=Api" },
                    { 7, false, null, 4, "https://tse1.mm.bing.net/th?id=OIP.PLvxFihIhkAe7oBQ_Ma-TwHaE7&pid=Api" }
                });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "Id", "CustomerId", "Discount", "EndDate", "LocationId", "StartDate" },
                values: new object[,]
                {
                    { 1, 1, 0f, new DateTime(2023, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 0f, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 0f, new DateTime(2023, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, 0f, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2023, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 0f, new DateTime(2023, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, 0f, new DateTime(2023, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 3, 0f, new DateTime(2023, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2023, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 4, 0f, new DateTime(2023, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2023, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 1, 0f, new DateTime(2023, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2023, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 1, 0f, new DateTime(2023, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 1, 0f, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 2, 0f, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_LandlordId",
                table: "Image",
                column: "LandlordId");

            migrationBuilder.CreateIndex(
                name: "IX_Image_LocationId",
                table: "Image",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_LandlordId",
                table: "Location",
                column: "LandlordId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CustomerId",
                table: "Reservation",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_LocationId",
                table: "Reservation",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Landlord");
        }
    }
}
