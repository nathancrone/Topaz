using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Topaz.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactActivityTypes",
                columns: table => new
                {
                    ContactActivityTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactActivityTypes", x => x.ContactActivityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "DoNotContactLetters",
                columns: table => new
                {
                    DoNotContactLetterId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportedDate = table.Column<DateTime>(nullable: true),
                    MailingAddress = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoNotContactLetters", x => x.DoNotContactLetterId);
                });

            migrationBuilder.CreateTable(
                name: "DoNotContactPhones",
                columns: table => new
                {
                    DoNotContactPhoneId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportedDate = table.Column<DateTime>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoNotContactPhones", x => x.DoNotContactPhoneId);
                });

            migrationBuilder.CreateTable(
                name: "PhoneResponseTypes",
                columns: table => new
                {
                    PhoneResponseTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneResponseTypes", x => x.PhoneResponseTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PhoneType",
                columns: table => new
                {
                    PhoneTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhoneType", x => x.PhoneTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Publishers",
                columns: table => new
                {
                    PublisherId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publishers", x => x.PublisherId);
                });

            migrationBuilder.CreateTable(
                name: "Territories",
                columns: table => new
                {
                    TerritoryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TerritoryCode = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true),
                    InActive = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Apartment_StreetTerritoryId = table.Column<int>(nullable: true),
                    Apartment_MapLocation = table.Column<string>(nullable: true),
                    Apartment_PropertyName = table.Column<string>(nullable: true),
                    Apartment_StreetAddress = table.Column<string>(nullable: true),
                    Business_MapLocation = table.Column<string>(nullable: true),
                    Inaccessible_StreetTerritoryId = table.Column<int>(nullable: true),
                    Street_MapLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Territories", x => x.TerritoryId);
                });

            migrationBuilder.CreateTable(
                name: "DoNotContactStreets",
                columns: table => new
                {
                    DoNotContactStreetId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TerritoryId = table.Column<int>(nullable: false),
                    ReportedDate = table.Column<DateTime>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    Coordinates = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoNotContactStreets", x => x.DoNotContactStreetId);
                    table.ForeignKey(
                        name: "FK_DoNotContactStreets_Territories_TerritoryId",
                        column: x => x.TerritoryId,
                        principalTable: "Territories",
                        principalColumn: "TerritoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InaccessibleAddresses",
                columns: table => new
                {
                    InaccessibleAddressId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TerritoryId = table.Column<int>(nullable: false),
                    CurrentContactListId = table.Column<int>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    EstimatedDwellingCount = table.Column<int>(nullable: false),
                    PropertyName = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InaccessibleAddresses", x => x.InaccessibleAddressId);
                    table.ForeignKey(
                        name: "FK_InaccessibleAddresses_Territories_TerritoryId",
                        column: x => x.TerritoryId,
                        principalTable: "Territories",
                        principalColumn: "TerritoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TerritoryActivities",
                columns: table => new
                {
                    TerritoryActivityId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TerritoryId = table.Column<int>(nullable: false),
                    PublisherId = table.Column<int>(nullable: false),
                    CheckOutDate = table.Column<DateTime>(nullable: true),
                    CheckInDate = table.Column<DateTime>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerritoryActivities", x => x.TerritoryActivityId);
                    table.ForeignKey(
                        name: "FK_TerritoryActivities_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TerritoryActivities_Territories_TerritoryId",
                        column: x => x.TerritoryId,
                        principalTable: "Territories",
                        principalColumn: "TerritoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InaccessibleContactLists",
                columns: table => new
                {
                    InaccessibleContactListId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InaccessibleAddressId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InaccessibleContactLists", x => x.InaccessibleContactListId);
                    table.ForeignKey(
                        name: "FK_InaccessibleContactLists_InaccessibleAddresses_InaccessibleAddressId",
                        column: x => x.InaccessibleAddressId,
                        principalTable: "InaccessibleAddresses",
                        principalColumn: "InaccessibleAddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InaccessibleContacts",
                columns: table => new
                {
                    InaccessibleContactId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InaccessibleContactListId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleInitial = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: true),
                    MailingAddress = table.Column<string>(nullable: true),
                    PhoneTypeId = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InaccessibleContacts", x => x.InaccessibleContactId);
                    table.ForeignKey(
                        name: "FK_InaccessibleContacts_InaccessibleContactLists_InaccessibleContactListId",
                        column: x => x.InaccessibleContactListId,
                        principalTable: "InaccessibleContactLists",
                        principalColumn: "InaccessibleContactListId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InaccessibleContacts_PhoneType_PhoneTypeId",
                        column: x => x.PhoneTypeId,
                        principalTable: "PhoneType",
                        principalColumn: "PhoneTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InaccessibleContactActivities",
                columns: table => new
                {
                    InaccessibleContactActivityId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InaccessibleContactId = table.Column<int>(nullable: false),
                    ActivityDate = table.Column<DateTime>(nullable: true),
                    ContactActivityTypeId = table.Column<int>(nullable: false),
                    PhoneResponseTypeId = table.Column<int>(nullable: false),
                    LetterReturned = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InaccessibleContactActivities", x => x.InaccessibleContactActivityId);
                    table.ForeignKey(
                        name: "FK_InaccessibleContactActivities_ContactActivityTypes_ContactActivityTypeId",
                        column: x => x.ContactActivityTypeId,
                        principalTable: "ContactActivityTypes",
                        principalColumn: "ContactActivityTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InaccessibleContactActivities_InaccessibleContacts_InaccessibleContactId",
                        column: x => x.InaccessibleContactId,
                        principalTable: "InaccessibleContacts",
                        principalColumn: "InaccessibleContactId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InaccessibleContactActivities_PhoneResponseTypes_PhoneResponseTypeId",
                        column: x => x.PhoneResponseTypeId,
                        principalTable: "PhoneResponseTypes",
                        principalColumn: "PhoneResponseTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ContactActivityTypes",
                columns: new[] { "ContactActivityTypeId", "Name" },
                values: new object[] { 1, "Phone" });

            migrationBuilder.InsertData(
                table: "ContactActivityTypes",
                columns: new[] { "ContactActivityTypeId", "Name" },
                values: new object[] { 2, "Letter" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 1, "Voicemail (no name)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 2, "Voicemail (name matches)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 3, "Voicemail (different name)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 4, "Fax / Modem" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 5, "Busy Signal" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 6, "Ring no answer" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 7, "Answered (\"not interested\")" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 8, "Answered (\"take me off your list\")" });

            migrationBuilder.InsertData(
                table: "PhoneType",
                columns: new[] { "PhoneTypeId", "Name" },
                values: new object[] { 1, "Mobile" });

            migrationBuilder.InsertData(
                table: "PhoneType",
                columns: new[] { "PhoneTypeId", "Name" },
                values: new object[] { 2, "Landline" });

            migrationBuilder.CreateIndex(
                name: "IX_DoNotContactStreets_TerritoryId",
                table: "DoNotContactStreets",
                column: "TerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleAddresses_TerritoryId",
                table: "InaccessibleAddresses",
                column: "TerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContactActivities_ContactActivityTypeId",
                table: "InaccessibleContactActivities",
                column: "ContactActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContactActivities_InaccessibleContactId",
                table: "InaccessibleContactActivities",
                column: "InaccessibleContactId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContactActivities_PhoneResponseTypeId",
                table: "InaccessibleContactActivities",
                column: "PhoneResponseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContactLists_InaccessibleAddressId",
                table: "InaccessibleContactLists",
                column: "InaccessibleAddressId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContacts_InaccessibleContactListId",
                table: "InaccessibleContacts",
                column: "InaccessibleContactListId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContacts_PhoneTypeId",
                table: "InaccessibleContacts",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TerritoryActivities_PublisherId",
                table: "TerritoryActivities",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_TerritoryActivities_TerritoryId",
                table: "TerritoryActivities",
                column: "TerritoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoNotContactLetters");

            migrationBuilder.DropTable(
                name: "DoNotContactPhones");

            migrationBuilder.DropTable(
                name: "DoNotContactStreets");

            migrationBuilder.DropTable(
                name: "InaccessibleContactActivities");

            migrationBuilder.DropTable(
                name: "TerritoryActivities");

            migrationBuilder.DropTable(
                name: "ContactActivityTypes");

            migrationBuilder.DropTable(
                name: "InaccessibleContacts");

            migrationBuilder.DropTable(
                name: "PhoneResponseTypes");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "InaccessibleContactLists");

            migrationBuilder.DropTable(
                name: "PhoneType");

            migrationBuilder.DropTable(
                name: "InaccessibleAddresses");

            migrationBuilder.DropTable(
                name: "Territories");
        }
    }
}
