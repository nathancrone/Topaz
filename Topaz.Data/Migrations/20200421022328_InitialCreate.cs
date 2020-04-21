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
                    MailingAddress1 = table.Column<string>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_Territories_Territories_Apartment_StreetTerritoryId",
                        column: x => x.Apartment_StreetTerritoryId,
                        principalTable: "Territories",
                        principalColumn: "TerritoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Territories_Territories_Inaccessible_StreetTerritoryId",
                        column: x => x.Inaccessible_StreetTerritoryId,
                        principalTable: "Territories",
                        principalColumn: "TerritoryId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "InaccessibleProperties",
                columns: table => new
                {
                    InaccessiblePropertyId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TerritoryId = table.Column<int>(nullable: false),
                    CurrentContactListId = table.Column<int>(nullable: true),
                    ResearchedDate = table.Column<DateTime>(nullable: true),
                    StreetNumbers = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    EstimatedDwellingCount = table.Column<int>(nullable: false),
                    PropertyName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InaccessibleProperties", x => x.InaccessiblePropertyId);
                    table.ForeignKey(
                        name: "FK_InaccessibleProperties_Territories_TerritoryId",
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
                    InaccessiblePropertyId = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InaccessibleContactLists", x => x.InaccessibleContactListId);
                    table.ForeignKey(
                        name: "FK_InaccessibleContactLists_InaccessibleProperties_InaccessiblePropertyId",
                        column: x => x.InaccessiblePropertyId,
                        principalTable: "InaccessibleProperties",
                        principalColumn: "InaccessiblePropertyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InaccessibleContacts",
                columns: table => new
                {
                    InaccessibleContactId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InaccessibleContactListId = table.Column<int>(nullable: false),
                    PublisherId = table.Column<int>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MiddleInitial = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: true),
                    MailingAddress1 = table.Column<string>(nullable: true),
                    MailingAddress2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    PhoneTypeId = table.Column<int>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    IsWorked = table.Column<bool>(nullable: false)
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InaccessibleContacts_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InaccessibleContactActivities",
                columns: table => new
                {
                    InaccessibleContactActivityId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InaccessibleContactId = table.Column<int>(nullable: false),
                    PublisherId = table.Column<int>(nullable: false),
                    ActivityDate = table.Column<DateTime>(nullable: true),
                    ContactActivityTypeId = table.Column<int>(nullable: false),
                    PhoneCallerIdBlocked = table.Column<bool>(nullable: false),
                    PhoneResponseTypeId = table.Column<int>(nullable: false),
                    LetterReturned = table.Column<DateTime>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_InaccessibleContactActivities_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
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
                values: new object[] { 14, "Answered (profanity or threatening)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 13, "Answered (\"Take me off your list\")" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 12, "Answered (\"Not Interested\")" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 11, "Answered (Hung up immediately)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 10, "Answered (\"Not a good time\")" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 9, "Answered (Responded favorably)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 8, "No Response (Ring no answer)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 6, "No Response (Busy Signal)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 5, "No Response (Fax / Modem)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 4, "Voicemail (Business Number)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 3, "Voicemail (Different Name)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 2, "Voicemail (Name Matches)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 1, "Voicemail (No Name)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Name" },
                values: new object[] { 7, "No Response (Not a working number)" });

            migrationBuilder.InsertData(
                table: "PhoneType",
                columns: new[] { "PhoneTypeId", "Name" },
                values: new object[] { 2, "Landline" });

            migrationBuilder.InsertData(
                table: "PhoneType",
                columns: new[] { "PhoneTypeId", "Name" },
                values: new object[] { 1, "Mobile" });

            migrationBuilder.InsertData(
                table: "PhoneType",
                columns: new[] { "PhoneTypeId", "Name" },
                values: new object[] { 3, "VOIP" });

            migrationBuilder.CreateIndex(
                name: "IX_DoNotContactStreets_TerritoryId",
                table: "DoNotContactStreets",
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
                name: "IX_InaccessibleContactActivities_PublisherId",
                table: "InaccessibleContactActivities",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContactLists_InaccessiblePropertyId",
                table: "InaccessibleContactLists",
                column: "InaccessiblePropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContacts_InaccessibleContactListId",
                table: "InaccessibleContacts",
                column: "InaccessibleContactListId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContacts_PhoneTypeId",
                table: "InaccessibleContacts",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContacts_PublisherId",
                table: "InaccessibleContacts",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleProperties_TerritoryId",
                table: "InaccessibleProperties",
                column: "TerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Territories_Apartment_StreetTerritoryId",
                table: "Territories",
                column: "Apartment_StreetTerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Territories_Inaccessible_StreetTerritoryId",
                table: "Territories",
                column: "Inaccessible_StreetTerritoryId");

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
                name: "InaccessibleContactLists");

            migrationBuilder.DropTable(
                name: "PhoneType");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "InaccessibleProperties");

            migrationBuilder.DropTable(
                name: "Territories");
        }
    }
}
