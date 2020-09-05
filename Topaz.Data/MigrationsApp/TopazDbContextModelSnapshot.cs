﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Topaz.Data;

namespace Topaz.Data.MigrationsApp
{
    [DbContext(typeof(TopazDbContext))]
    partial class TopazDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("Topaz.Common.Models.ContactActivityType", b =>
                {
                    b.Property<int>("ContactActivityTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ContactActivityTypeId");

                    b.ToTable("ContactActivityTypes");

                    b.HasData(
                        new
                        {
                            ContactActivityTypeId = 1,
                            Name = "Phone (don't leave a voicemail)"
                        },
                        new
                        {
                            ContactActivityTypeId = 2,
                            Name = "Phone (leave a voicemail)"
                        },
                        new
                        {
                            ContactActivityTypeId = 3,
                            Name = "Letter"
                        },
                        new
                        {
                            ContactActivityTypeId = 4,
                            Name = "Email"
                        },
                        new
                        {
                            ContactActivityTypeId = 5,
                            Name = "Text"
                        });
                });

            modelBuilder.Entity("Topaz.Common.Models.DoNotContactLetter", b =>
                {
                    b.Property<int>("DoNotContactLetterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MailingAddress1")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReportedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("DoNotContactLetterId");

                    b.ToTable("DoNotContactLetters");
                });

            modelBuilder.Entity("Topaz.Common.Models.DoNotContactPhone", b =>
                {
                    b.Property<int>("DoNotContactPhoneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReportedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("DoNotContactPhoneId");

                    b.ToTable("DoNotContactPhones");
                });

            modelBuilder.Entity("Topaz.Common.Models.DoNotContactStreet", b =>
                {
                    b.Property<int>("DoNotContactStreetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Coordinates")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ReportedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("TEXT");

                    b.Property<int>("TerritoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("DoNotContactStreetId");

                    b.HasIndex("TerritoryId");

                    b.ToTable("DoNotContactStreets");
                });

            modelBuilder.Entity("Topaz.Common.Models.InaccessibleContact", b =>
                {
                    b.Property<int>("InaccessibleContactId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AssignContactActivityTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("AssignDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("AssignPublisherId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddresses")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<int>("InaccessibleContactListId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("MailingAddress1")
                        .HasColumnType("TEXT");

                    b.Property<string>("MailingAddress2")
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleInitial")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<int?>("PhoneTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PostalCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.HasKey("InaccessibleContactId");

                    b.HasIndex("AssignPublisherId");

                    b.HasIndex("InaccessibleContactListId");

                    b.HasIndex("PhoneTypeId");

                    b.ToTable("InaccessibleContacts");
                });

            modelBuilder.Entity("Topaz.Common.Models.InaccessibleContactActivity", b =>
                {
                    b.Property<int>("InaccessibleContactActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ActivityDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("ContactActivityTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("InaccessibleContactId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LetterReturnDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneCallerIdBlocked")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PhoneResponseTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PublisherId")
                        .HasColumnType("INTEGER");

                    b.HasKey("InaccessibleContactActivityId");

                    b.HasIndex("ContactActivityTypeId");

                    b.HasIndex("InaccessibleContactId");

                    b.HasIndex("PhoneResponseTypeId");

                    b.HasIndex("PublisherId");

                    b.ToTable("InaccessibleContactActivities");
                });

            modelBuilder.Entity("Topaz.Common.Models.InaccessibleContactList", b =>
                {
                    b.Property<int>("InaccessibleContactListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("InaccessiblePropertyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("InaccessibleContactListId");

                    b.HasIndex("InaccessiblePropertyId");

                    b.ToTable("InaccessibleContactLists");
                });

            modelBuilder.Entity("Topaz.Common.Models.InaccessibleProperty", b =>
                {
                    b.Property<int>("InaccessiblePropertyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CurrentContactListId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int>("EstimatedDwellingCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PostalCode")
                        .HasColumnType("TEXT");

                    b.Property<string>("PropertyName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ResearchedDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetNumbers")
                        .HasColumnType("TEXT");

                    b.Property<int>("TerritoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("InaccessiblePropertyId");

                    b.HasIndex("TerritoryId");

                    b.ToTable("InaccessibleProperties");
                });

            modelBuilder.Entity("Topaz.Common.Models.PhoneResponseType", b =>
                {
                    b.Property<int>("PhoneResponseTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("PhoneResponseTypeId");

                    b.ToTable("PhoneResponseTypes");

                    b.HasData(
                        new
                        {
                            PhoneResponseTypeId = 100,
                            Name = "Voicemail (No Name)"
                        },
                        new
                        {
                            PhoneResponseTypeId = 101,
                            Name = "Voicemail (Name Matches)"
                        },
                        new
                        {
                            PhoneResponseTypeId = 102,
                            Name = "Voicemail (Different Name)"
                        },
                        new
                        {
                            PhoneResponseTypeId = 103,
                            Name = "Voicemail (Business Number)"
                        },
                        new
                        {
                            PhoneResponseTypeId = 104,
                            Name = "Voicemail (Mailbox Full or Not Set Up)"
                        },
                        new
                        {
                            PhoneResponseTypeId = 200,
                            Name = "No Response (Fax / Modem)"
                        },
                        new
                        {
                            PhoneResponseTypeId = 201,
                            Name = "No Response (Busy Signal)"
                        },
                        new
                        {
                            PhoneResponseTypeId = 202,
                            Name = "No Response (Not a working number)"
                        },
                        new
                        {
                            PhoneResponseTypeId = 203,
                            Name = "No Response (Ring no answer)"
                        },
                        new
                        {
                            PhoneResponseTypeId = 300,
                            Name = "Answered (Responded favorably)"
                        },
                        new
                        {
                            PhoneResponseTypeId = 301,
                            Name = "Answered (\"Not a good time\")"
                        },
                        new
                        {
                            PhoneResponseTypeId = 302,
                            Name = "Answered (Hung up immediately)"
                        },
                        new
                        {
                            PhoneResponseTypeId = 303,
                            Name = "Answered (\"Not Interested\")"
                        },
                        new
                        {
                            PhoneResponseTypeId = 304,
                            Name = "Answered (\"Take me off your list\")"
                        },
                        new
                        {
                            PhoneResponseTypeId = 305,
                            Name = "Answered (profanity or threatening)"
                        },
                        new
                        {
                            PhoneResponseTypeId = 306,
                            Name = "Answered (doesn't speak English)"
                        });
                });

            modelBuilder.Entity("Topaz.Common.Models.PhoneType", b =>
                {
                    b.Property<int>("PhoneTypeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("PhoneTypeId");

                    b.ToTable("PhoneType");

                    b.HasData(
                        new
                        {
                            PhoneTypeId = 1,
                            Name = "Mobile"
                        },
                        new
                        {
                            PhoneTypeId = 2,
                            Name = "Landline"
                        },
                        new
                        {
                            PhoneTypeId = 3,
                            Name = "Voip"
                        });
                });

            modelBuilder.Entity("Topaz.Common.Models.Publisher", b =>
                {
                    b.Property<int>("PublisherId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("PublisherId");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("Topaz.Common.Models.Territory", b =>
                {
                    b.Property<int>("TerritoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("InActive")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<string>("TerritoryCode")
                        .HasColumnType("TEXT");

                    b.HasKey("TerritoryId");

                    b.ToTable("Territories");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Territory");
                });

            modelBuilder.Entity("Topaz.Common.Models.TerritoryActivity", b =>
                {
                    b.Property<int>("TerritoryActivityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("CheckInDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CheckOutDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<int>("PublisherId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TerritoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("TerritoryActivityId");

                    b.HasIndex("PublisherId");

                    b.HasIndex("TerritoryId");

                    b.ToTable("TerritoryActivities");
                });

            modelBuilder.Entity("Topaz.Common.Models.ApartmentTerritory", b =>
                {
                    b.HasBaseType("Topaz.Common.Models.Territory");

                    b.Property<string>("MapLocation")
                        .HasColumnName("Apartment_MapLocation")
                        .HasColumnType("TEXT");

                    b.Property<string>("PropertyName")
                        .HasColumnName("Apartment_PropertyName")
                        .HasColumnType("TEXT");

                    b.Property<string>("StreetAddress")
                        .HasColumnName("Apartment_StreetAddress")
                        .HasColumnType("TEXT");

                    b.Property<int>("StreetTerritoryId")
                        .HasColumnName("Apartment_StreetTerritoryId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("StreetTerritoryId");

                    b.HasDiscriminator().HasValue("ApartmentTerritory");
                });

            modelBuilder.Entity("Topaz.Common.Models.BusinessTerritory", b =>
                {
                    b.HasBaseType("Topaz.Common.Models.Territory");

                    b.Property<string>("MapLocation")
                        .HasColumnName("Business_MapLocation")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("BusinessTerritory");
                });

            modelBuilder.Entity("Topaz.Common.Models.InaccessibleTerritory", b =>
                {
                    b.HasBaseType("Topaz.Common.Models.Territory");

                    b.Property<int>("StreetTerritoryId")
                        .HasColumnName("Inaccessible_StreetTerritoryId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("StreetTerritoryId");

                    b.HasDiscriminator().HasValue("InaccessibleTerritory");
                });

            modelBuilder.Entity("Topaz.Common.Models.StreetTerritory", b =>
                {
                    b.HasBaseType("Topaz.Common.Models.Territory");

                    b.Property<string>("MapLocation")
                        .HasColumnName("Street_MapLocation")
                        .HasColumnType("TEXT");

                    b.Property<int?>("RefId")
                        .HasColumnName("Street_RefId")
                        .HasColumnType("INTEGER");

                    b.HasDiscriminator().HasValue("StreetTerritory");
                });

            modelBuilder.Entity("Topaz.Common.Models.DoNotContactStreet", b =>
                {
                    b.HasOne("Topaz.Common.Models.StreetTerritory", "Territory")
                        .WithMany()
                        .HasForeignKey("TerritoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Topaz.Common.Models.InaccessibleContact", b =>
                {
                    b.HasOne("Topaz.Common.Models.Publisher", "AssignPublisher")
                        .WithMany("InaccessibleContacts")
                        .HasForeignKey("AssignPublisherId");

                    b.HasOne("Topaz.Common.Models.InaccessibleContactList", "ContactList")
                        .WithMany("Contacts")
                        .HasForeignKey("InaccessibleContactListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Topaz.Common.Models.PhoneType", "PhoneType")
                        .WithMany("InaccessibleContacts")
                        .HasForeignKey("PhoneTypeId");
                });

            modelBuilder.Entity("Topaz.Common.Models.InaccessibleContactActivity", b =>
                {
                    b.HasOne("Topaz.Common.Models.ContactActivityType", "ContactActivityType")
                        .WithMany("ContactActivity")
                        .HasForeignKey("ContactActivityTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Topaz.Common.Models.InaccessibleContact", "Contact")
                        .WithMany("ContactActivity")
                        .HasForeignKey("InaccessibleContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Topaz.Common.Models.PhoneResponseType", "PhoneResponseType")
                        .WithMany("ContactActivity")
                        .HasForeignKey("PhoneResponseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Topaz.Common.Models.Publisher", "Publisher")
                        .WithMany("InaccessibleContactActivity")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Topaz.Common.Models.InaccessibleContactList", b =>
                {
                    b.HasOne("Topaz.Common.Models.InaccessibleProperty", "Property")
                        .WithMany("ContactLists")
                        .HasForeignKey("InaccessiblePropertyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Topaz.Common.Models.InaccessibleProperty", b =>
                {
                    b.HasOne("Topaz.Common.Models.InaccessibleTerritory", "Territory")
                        .WithMany("InaccessibleProperties")
                        .HasForeignKey("TerritoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Topaz.Common.Models.TerritoryActivity", b =>
                {
                    b.HasOne("Topaz.Common.Models.Publisher", "Publisher")
                        .WithMany("Activity")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Topaz.Common.Models.ApartmentTerritory", "ApartmentTerritory")
                        .WithMany("Activity")
                        .HasForeignKey("TerritoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Topaz.Common.Models.BusinessTerritory", "BusinessTerritory")
                        .WithMany("Activity")
                        .HasForeignKey("TerritoryId")
                        .HasConstraintName("FK_TerritoryActivities_Territories_TerritoryId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Topaz.Common.Models.InaccessibleTerritory", "InaccessibleTerritory")
                        .WithMany("Activity")
                        .HasForeignKey("TerritoryId")
                        .HasConstraintName("FK_TerritoryActivities_Territories_TerritoryId2")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Topaz.Common.Models.StreetTerritory", "StreetTerritory")
                        .WithMany("Activity")
                        .HasForeignKey("TerritoryId")
                        .HasConstraintName("FK_TerritoryActivities_Territories_TerritoryId3")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Topaz.Common.Models.ApartmentTerritory", b =>
                {
                    b.HasOne("Topaz.Common.Models.StreetTerritory", "StreetTerritory")
                        .WithMany("ApartmentTerritories")
                        .HasForeignKey("StreetTerritoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Topaz.Common.Models.InaccessibleTerritory", b =>
                {
                    b.HasOne("Topaz.Common.Models.StreetTerritory", "StreetTerritory")
                        .WithMany("InaccessibleTerritories")
                        .HasForeignKey("StreetTerritoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
