using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Topaz.Data.MigrationsApp
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
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactActivityTypes", x => x.ContactActivityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "PhoneResponseTypes",
                columns: table => new
                {
                    PhoneResponseTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
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
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
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
                    LastName = table.Column<string>(nullable: true),
                    IsHidden = table.Column<bool>(nullable: false)
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
                    Street_MapLocation = table.Column<string>(nullable: true),
                    Street_RefId = table.Column<int>(nullable: true)
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
                name: "DoNotContactPhones",
                columns: table => new
                {
                    DoNotContactPhoneId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PublisherId = table.Column<int>(nullable: false),
                    ReportedDate = table.Column<DateTime>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoNotContactPhones", x => x.DoNotContactPhoneId);
                    table.ForeignKey(
                        name: "FK_DoNotContactPhones_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoNotContactLetters",
                columns: table => new
                {
                    DoNotContactLetterId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TerritoryId = table.Column<int>(nullable: false),
                    PublisherId = table.Column<int>(nullable: false),
                    ReportedDate = table.Column<DateTime>(nullable: true),
                    MailingAddress1 = table.Column<string>(nullable: true),
                    MailingAddress2 = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoNotContactLetters", x => x.DoNotContactLetterId);
                    table.ForeignKey(
                        name: "FK_DoNotContactLetters_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoNotContactLetters_Territories_TerritoryId",
                        column: x => x.TerritoryId,
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
                    PublisherId = table.Column<int>(nullable: false),
                    ReportedDate = table.Column<DateTime>(nullable: true),
                    StreetAddress = table.Column<string>(nullable: true),
                    Coordinates = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoNotContactStreets", x => x.DoNotContactStreetId);
                    table.ForeignKey(
                        name: "FK_DoNotContactStreets_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "InaccessibleTerritoryExports",
                columns: table => new
                {
                    InaccessibleTerritoryExportId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TerritoryId = table.Column<int>(nullable: false),
                    PublisherId = table.Column<int>(nullable: false),
                    ExportDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InaccessibleTerritoryExports", x => x.InaccessibleTerritoryExportId);
                    table.ForeignKey(
                        name: "FK_InaccessibleTerritoryExports_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InaccessibleTerritoryExports_Territories_TerritoryId",
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
                    table.ForeignKey(
                        name: "FK_TerritoryActivities_Territories_TerritoryId1",
                        column: x => x.TerritoryId,
                        principalTable: "Territories",
                        principalColumn: "TerritoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TerritoryActivities_Territories_TerritoryId2",
                        column: x => x.TerritoryId,
                        principalTable: "Territories",
                        principalColumn: "TerritoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TerritoryActivities_Territories_TerritoryId3",
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
                name: "InaccessibleTerritoryExportItems",
                columns: table => new
                {
                    InaccessibleTerritoryExportItemId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InaccessibleTerritoryExportId = table.Column<int>(nullable: false),
                    InaccessibleContactId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InaccessibleTerritoryExportItems", x => x.InaccessibleTerritoryExportItemId);
                    table.ForeignKey(
                        name: "FK_InaccessibleTerritoryExportItems_InaccessibleTerritoryExports_InaccessibleTerritoryExportId",
                        column: x => x.InaccessibleTerritoryExportId,
                        principalTable: "InaccessibleTerritoryExports",
                        principalColumn: "InaccessibleTerritoryExportId",
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
                    MailingAddress1 = table.Column<string>(nullable: true),
                    MailingAddress2 = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    PhoneTypeId = table.Column<int>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    EmailAddresses = table.Column<string>(nullable: true),
                    AssignPublisherId = table.Column<int>(nullable: true),
                    AssignDate = table.Column<DateTime>(nullable: true),
                    AssignContactActivityTypeId = table.Column<int>(nullable: true),
                    InaccessibleTerritoryExportItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InaccessibleContacts", x => x.InaccessibleContactId);
                    table.ForeignKey(
                        name: "FK_InaccessibleContacts_Publishers_AssignPublisherId",
                        column: x => x.AssignPublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InaccessibleContacts_InaccessibleContactLists_InaccessibleContactListId",
                        column: x => x.InaccessibleContactListId,
                        principalTable: "InaccessibleContactLists",
                        principalColumn: "InaccessibleContactListId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InaccessibleContacts_InaccessibleTerritoryExportItems_InaccessibleTerritoryExportItemId",
                        column: x => x.InaccessibleTerritoryExportItemId,
                        principalTable: "InaccessibleTerritoryExportItems",
                        principalColumn: "InaccessibleTerritoryExportItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InaccessibleContacts_PhoneType_PhoneTypeId",
                        column: x => x.PhoneTypeId,
                        principalTable: "PhoneType",
                        principalColumn: "PhoneTypeId",
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
                    InaccessibleTerritoryExportId = table.Column<int>(nullable: true),
                    ActivityDate = table.Column<DateTime>(nullable: true),
                    ContactActivityTypeId = table.Column<int>(nullable: false),
                    PhoneCallerIdBlocked = table.Column<bool>(nullable: false),
                    PhoneResponseTypeId = table.Column<int>(nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InaccessibleContactActivities_Publishers_PublisherId",
                        column: x => x.PublisherId,
                        principalTable: "Publishers",
                        principalColumn: "PublisherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ContactActivityTypes",
                columns: new[] { "ContactActivityTypeId", "Description", "Name" },
                values: new object[] { 1, "Contact this person via telephone. DO NOT leave a message if the phone call goes to voicemail.", "Phone (don't leave a voicemail)" });

            migrationBuilder.InsertData(
                table: "ContactActivityTypes",
                columns: new[] { "ContactActivityTypeId", "Description", "Name" },
                values: new object[] { 2, "Contact this person via telephone. Leave a message if the phone call goes to voicemail.", "Phone (leave a voicemail)" });

            migrationBuilder.InsertData(
                table: "ContactActivityTypes",
                columns: new[] { "ContactActivityTypeId", "Description", "Name" },
                values: new object[] { 3, "Write a letter to this person.", "Letter Sent" });

            migrationBuilder.InsertData(
                table: "ContactActivityTypes",
                columns: new[] { "ContactActivityTypeId", "Description", "Name" },
                values: new object[] { 4, "Designates that the letter was returned without reaching the intended recipient.", "Letter Returned" });

            migrationBuilder.InsertData(
                table: "ContactActivityTypes",
                columns: new[] { "ContactActivityTypeId", "Description", "Name" },
                values: new object[] { 5, "Send an email to this person.", "Email" });

            migrationBuilder.InsertData(
                table: "ContactActivityTypes",
                columns: new[] { "ContactActivityTypeId", "Description", "Name" },
                values: new object[] { 6, "Send a text message to this person.", "Text" });

            migrationBuilder.InsertData(
                table: "ContactActivityTypes",
                columns: new[] { "ContactActivityTypeId", "Description", "Name" },
                values: new object[] { 8, "Contact exported to be worked externally.", "Export" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 308, "Someone picked up the phone. The contact was a business and was unable to speak.", "Answered (Business)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 307, "Someone picked up the phone. Was unable to communicate because they didn't speak English.", "Answered (doesn't speak English)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 306, "Someone picked up the phone. The person was agitated. They possibly used rude, threatening, or profane language.", "Answered (profanity or threatening)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 305, "Someone picked up the phone. The contact specifically requested to be removed from the calling list.", "Answered (\"Take me off your list\")" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 304, "Someone picked up the phone. The contact indicated that they weren't interested.", "Answered (\"Not Interested\")" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 303, "Someone picked up the phone. You were able to introduce yourself. The person hung up.", "Answered (Listened then hung up)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 302, "Someone picked up the phone. The call immediately disconnected (the caller likely hung up instantly after answering). No communication occurred.", "Answered (Hung up immediately)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 301, "The caller successfully spoke to a person. The contact stated that they weren't able to talk right now. A call back later would be appropriate.", "Answered (\"Not a good time\")" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 300, "The caller successfully spoke to a person. The call was positive. This contact will be considered complete. The caller will retain this call for their personal records if they feel a follow up would be appropriate.", "Answered (Responded favorably)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 202, "The call attempt was unsuccessful. The caller got an automated message indicating that this is not a working number.", "No Response (Not a working number)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 203, "The call attempt was unsuccessful. The caller let the phone ring multiple times but nobody answered.", "No Response (Ring no answer)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 201, "The call attempt was unsuccessful. The caller heard a busy signal.", "No Response (Busy Signal)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 200, "The call attempt was unsuccessful. The caller heard a fax or modem signal.", "No Response (Fax / Modem)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 104, "The call went to voicemail but the voicemail account was either not set up or was full.", "Voicemail (Mailbox Full or Not Set Up)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 103, "The call went to voicemail. The voicemail message was for a business.", "Voicemail (Business Number)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 102, "The call went to voicemail. The name given in the voicemail message is different from the contact information.", "Voicemail (Different Name)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 101, "The call went to voicemail. The name given in the voicemail message matches the contact information.", "Voicemail (Name Matches)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 100, "The call went to voicemail. The voicemail message did not give a name.", "Voicemail (No Name)" });

            migrationBuilder.InsertData(
                table: "PhoneResponseTypes",
                columns: new[] { "PhoneResponseTypeId", "Description", "Name" },
                values: new object[] { 204, "The call attempt was unsuccessful. Message indicating that the number is not accepting calls.", "No Response (Not accepting calls)" });

            migrationBuilder.InsertData(
                table: "PhoneType",
                columns: new[] { "PhoneTypeId", "Description", "Name" },
                values: new object[] { 2, null, "Landline" });

            migrationBuilder.InsertData(
                table: "PhoneType",
                columns: new[] { "PhoneTypeId", "Description", "Name" },
                values: new object[] { 1, null, "Mobile" });

            migrationBuilder.InsertData(
                table: "PhoneType",
                columns: new[] { "PhoneTypeId", "Description", "Name" },
                values: new object[] { 3, null, "Voip" });

            migrationBuilder.CreateIndex(
                name: "IX_DoNotContactLetters_PublisherId",
                table: "DoNotContactLetters",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_DoNotContactLetters_TerritoryId",
                table: "DoNotContactLetters",
                column: "TerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DoNotContactLetters_MailingAddress1_MailingAddress2",
                table: "DoNotContactLetters",
                columns: new[] { "MailingAddress1", "MailingAddress2" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoNotContactPhones_PhoneNumber",
                table: "DoNotContactPhones",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoNotContactPhones_PublisherId",
                table: "DoNotContactPhones",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_DoNotContactStreets_PublisherId",
                table: "DoNotContactStreets",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_DoNotContactStreets_StreetAddress",
                table: "DoNotContactStreets",
                column: "StreetAddress",
                unique: true);

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
                name: "IX_InaccessibleContacts_AssignPublisherId",
                table: "InaccessibleContacts",
                column: "AssignPublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContacts_InaccessibleContactListId",
                table: "InaccessibleContacts",
                column: "InaccessibleContactListId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContacts_InaccessibleTerritoryExportItemId",
                table: "InaccessibleContacts",
                column: "InaccessibleTerritoryExportItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleContacts_PhoneTypeId",
                table: "InaccessibleContacts",
                column: "PhoneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleProperties_TerritoryId",
                table: "InaccessibleProperties",
                column: "TerritoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleTerritoryExportItems_InaccessibleTerritoryExportId",
                table: "InaccessibleTerritoryExportItems",
                column: "InaccessibleTerritoryExportId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleTerritoryExports_PublisherId",
                table: "InaccessibleTerritoryExports",
                column: "PublisherId");

            migrationBuilder.CreateIndex(
                name: "IX_InaccessibleTerritoryExports_TerritoryId",
                table: "InaccessibleTerritoryExports",
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
                name: "IX_Territories_TerritoryCode",
                table: "Territories",
                column: "TerritoryCode",
                unique: true);

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
                name: "InaccessibleTerritoryExportItems");

            migrationBuilder.DropTable(
                name: "PhoneType");

            migrationBuilder.DropTable(
                name: "InaccessibleProperties");

            migrationBuilder.DropTable(
                name: "InaccessibleTerritoryExports");

            migrationBuilder.DropTable(
                name: "Publishers");

            migrationBuilder.DropTable(
                name: "Territories");
        }
    }
}
