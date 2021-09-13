﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using e_Mekteb.ApDbContext;

namespace e_Mekteb.Migrations
{
    [DbContext(typeof(e_MektebDbContext))]
    partial class e_MektebDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("e_Mekteb.Models.Adresa", b =>
                {
                    b.Property<int>("AdresaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NazivMjesta")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PostanskiBroj")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("Ulica")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("AdresaId");

                    b.ToTable("Adrese");
                });

            modelBuilder.Entity("e_Mekteb.Models.Aktivnost", b =>
                {
                    b.Property<int>("AktivnostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("SkolskaGodinaId")
                        .HasColumnType("int");

                    b.Property<int>("TipAktivnosti")
                        .HasColumnType("int");

                    b.HasKey("AktivnostId");

                    b.HasIndex("SkolskaGodinaId");

                    b.ToTable("Aktivnosti");
                });

            modelBuilder.Entity("e_Mekteb.Models.AplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("BrojMobitela")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DatumRodenja")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ImeiPrezime")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("ImeiPrezimeRoditelja")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("MedzlisId")
                        .HasColumnType("int");

                    b.Property<string>("NazivMjesta")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PostanskiBroj")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Spol")
                        .HasColumnType("int");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Ulica")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("AplicationUserId");

                    b.HasIndex("MedzlisId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("e_Mekteb.Models.Biljeska", b =>
                {
                    b.Property<int>("BiljeskaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AktivnostId")
                        .HasColumnType("int");

                    b.Property<string>("AplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Biljeske")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.HasKey("BiljeskaId");

                    b.HasIndex("AktivnostId");

                    b.HasIndex("AplicationUserId");

                    b.ToTable("Biljeske");
                });

            modelBuilder.Entity("e_Mekteb.Models.ClanMualimskogVijeca", b =>
                {
                    b.Property<int>("ClanMualimskogVijecaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailClanaVijeca")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImeIPrezimeClanaVijeca")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("KontaktClanaVijeca")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)")
                        .HasMaxLength(12);

                    b.Property<int>("MedzlisId")
                        .HasColumnType("int");

                    b.HasKey("ClanMualimskogVijecaId");

                    b.HasIndex("MedzlisId");

                    b.ToTable("ClanoviMualimskogVijeca");
                });

            modelBuilder.Entity("e_Mekteb.Models.Medzlis", b =>
                {
                    b.Property<int>("MedzlisId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AdresaId")
                        .HasColumnType("int");

                    b.Property<string>("EmailGlavnogImama")
                        .IsRequired()
                        .HasColumnType("nvarchar(254)")
                        .HasMaxLength(254);

                    b.Property<string>("GlavniImam")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Kontakt")
                        .IsRequired()
                        .HasColumnType("nvarchar(12)")
                        .HasMaxLength(12);

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("MedzlisId");

                    b.HasIndex("AdresaId");

                    b.ToTable("Medzlisi");
                });

            modelBuilder.Entity("e_Mekteb.Models.Obavijest", b =>
                {
                    b.Property<int>("ObavijestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sadrzaj")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("VjerouciteljId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ObavijestId");

                    b.HasIndex("VjerouciteljId");

                    b.ToTable("Obavijesti");
                });

            modelBuilder.Entity("e_Mekteb.Models.Prisutnost", b =>
                {
                    b.Property<int>("PrisutnostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AktivnostId")
                        .HasColumnType("int");

                    b.Property<string>("AplicationUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<string>("IsPrisutan")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PrisutnostId");

                    b.HasIndex("AktivnostId");

                    b.HasIndex("AplicationUserId");

                    b.ToTable("Prisutnosti");
                });

            modelBuilder.Entity("e_Mekteb.Models.Razred", b =>
                {
                    b.Property<int>("RazredId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("MedzlisId")
                        .HasColumnType("int");

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("SkolskaGodinaId")
                        .HasColumnType("int");

                    b.HasKey("RazredId");

                    b.HasIndex("MedzlisId");

                    b.HasIndex("SkolskaGodinaId");

                    b.ToTable("Razredi");
                });

            modelBuilder.Entity("e_Mekteb.Models.RazredUcenik", b =>
                {
                    b.Property<int>("RazredUcenikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DatumIspisa")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumUpisa")
                        .HasColumnType("datetime2");

                    b.Property<int>("MedzlisId")
                        .HasColumnType("int");

                    b.Property<string>("Razred")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SkolaId")
                        .HasColumnType("int");

                    b.Property<int>("SkolskaGodinaId")
                        .HasColumnType("int");

                    b.Property<string>("UcenikId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VjerouciteljId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RazredUcenikId");

                    b.HasIndex("SkolaId");

                    b.HasIndex("SkolskaGodinaId");

                    b.ToTable("RazrediUcenik");
                });

            modelBuilder.Entity("e_Mekteb.Models.Skola", b =>
                {
                    b.Property<int>("SkolaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Adresa")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grad")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MedzlisId")
                        .HasColumnType("int");

                    b.Property<string>("NazivSkole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostanskiBroj")
                        .HasColumnType("int");

                    b.Property<string>("VjerouciteljId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SkolaId");

                    b.HasIndex("MedzlisId");

                    b.ToTable("Skole");
                });

            modelBuilder.Entity("e_Mekteb.Models.SkolaUcenik", b =>
                {
                    b.Property<int>("SkolaUcenikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NazivSkole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SkolaId")
                        .HasColumnType("int");

                    b.Property<string>("UcenikId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VjerouciteljId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SkolaUcenikId");

                    b.HasIndex("SkolaId");

                    b.ToTable("SkoleUcenika");
                });

            modelBuilder.Entity("e_Mekteb.Models.SkolskaGodina", b =>
                {
                    b.Property<int>("SkolskaGodinaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Godina")
                        .IsRequired()
                        .HasColumnType("nvarchar(9)")
                        .HasMaxLength(9);

                    b.HasKey("SkolskaGodinaId");

                    b.ToTable("SkolskeGodine");
                });

            modelBuilder.Entity("e_Mekteb.Models.UcenikAktivnost", b =>
                {
                    b.Property<string>("UcenikId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AktivnostId")
                        .HasColumnType("int");

                    b.Property<string>("NazivPredmeta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UcenikAktivnostId")
                        .HasColumnType("int");

                    b.Property<string>("VjerouciteljId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UcenikId", "AktivnostId");

                    b.HasIndex("AktivnostId");

                    b.ToTable("Pohada");
                });

            modelBuilder.Entity("e_Mekteb.Models.VjerouciteljAktivnost", b =>
                {
                    b.Property<string>("VjerouciteljId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AktivnostId")
                        .HasColumnType("int");

                    b.Property<string>("NazivPredmeta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VjerouciteljAktivnostId")
                        .HasColumnType("int");

                    b.HasKey("VjerouciteljId", "AktivnostId");

                    b.HasIndex("AktivnostId");

                    b.ToTable("Predaje");
                });

            modelBuilder.Entity("e_Mekteb.Models.VjerouciteljUcenik", b =>
                {
                    b.Property<int>("VjerouciteljUcenikId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UcenikId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VjerouciteljId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("VjerouciteljUcenikId");

                    b.HasIndex("UcenikId");

                    b.HasIndex("VjerouciteljId");

                    b.ToTable("VjerouciteljUcenik");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("e_Mekteb.Models.AplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("e_Mekteb.Models.AplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("e_Mekteb.Models.AplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("e_Mekteb.Models.AplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("e_Mekteb.Models.Aktivnost", b =>
                {
                    b.HasOne("e_Mekteb.Models.SkolskaGodina", "SkolskaGodina")
                        .WithMany("Aktivnosti")
                        .HasForeignKey("SkolskaGodinaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("e_Mekteb.Models.AplicationUser", b =>
                {
                    b.HasOne("e_Mekteb.Models.AplicationUser", null)
                        .WithMany("Ucenici")
                        .HasForeignKey("AplicationUserId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("e_Mekteb.Models.Medzlis", null)
                        .WithMany("Vjeroucitelji")
                        .HasForeignKey("MedzlisId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("e_Mekteb.Models.Biljeska", b =>
                {
                    b.HasOne("e_Mekteb.Models.Aktivnost", "Aktivnost")
                        .WithMany("Biljeske")
                        .HasForeignKey("AktivnostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("e_Mekteb.Models.AplicationUser", "AplicationUser")
                        .WithMany()
                        .HasForeignKey("AplicationUserId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("e_Mekteb.Models.ClanMualimskogVijeca", b =>
                {
                    b.HasOne("e_Mekteb.Models.Medzlis", "Medzlis")
                        .WithMany("ClanoviMualimskogVijeca")
                        .HasForeignKey("MedzlisId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("e_Mekteb.Models.Medzlis", b =>
                {
                    b.HasOne("e_Mekteb.Models.Adresa", "Adresa")
                        .WithMany()
                        .HasForeignKey("AdresaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("e_Mekteb.Models.Obavijest", b =>
                {
                    b.HasOne("e_Mekteb.Models.AplicationUser", "Vjeroucitelj")
                        .WithMany()
                        .HasForeignKey("VjerouciteljId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("e_Mekteb.Models.Prisutnost", b =>
                {
                    b.HasOne("e_Mekteb.Models.Aktivnost", "Aktivnost")
                        .WithMany("Prisutnosti")
                        .HasForeignKey("AktivnostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("e_Mekteb.Models.AplicationUser", "AplicationUser")
                        .WithMany()
                        .HasForeignKey("AplicationUserId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("e_Mekteb.Models.Razred", b =>
                {
                    b.HasOne("e_Mekteb.Models.Medzlis", null)
                        .WithMany("Razredi")
                        .HasForeignKey("MedzlisId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("e_Mekteb.Models.SkolskaGodina", null)
                        .WithMany("Razredi")
                        .HasForeignKey("SkolskaGodinaId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("e_Mekteb.Models.RazredUcenik", b =>
                {
                    b.HasOne("e_Mekteb.Models.Skola", "Skola")
                        .WithMany()
                        .HasForeignKey("SkolaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("e_Mekteb.Models.SkolskaGodina", "SkolskaGodina")
                        .WithMany()
                        .HasForeignKey("SkolskaGodinaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("e_Mekteb.Models.Skola", b =>
                {
                    b.HasOne("e_Mekteb.Models.Medzlis", "Medzlis")
                        .WithMany()
                        .HasForeignKey("MedzlisId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("e_Mekteb.Models.SkolaUcenik", b =>
                {
                    b.HasOne("e_Mekteb.Models.Skola", "Skola")
                        .WithMany()
                        .HasForeignKey("SkolaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("e_Mekteb.Models.UcenikAktivnost", b =>
                {
                    b.HasOne("e_Mekteb.Models.Aktivnost", "Aktivnost")
                        .WithMany("UcenickeAktivnosti")
                        .HasForeignKey("AktivnostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("e_Mekteb.Models.AplicationUser", "Ucenik")
                        .WithMany("UcenickeAktivnosti")
                        .HasForeignKey("UcenikId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("e_Mekteb.Models.VjerouciteljAktivnost", b =>
                {
                    b.HasOne("e_Mekteb.Models.Aktivnost", "Aktivnost")
                        .WithMany("VjerouciteljskeAktivnosti")
                        .HasForeignKey("AktivnostId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("e_Mekteb.Models.AplicationUser", "Vjeroucitelj")
                        .WithMany("VjerouciteljskeAktivnosti")
                        .HasForeignKey("VjerouciteljId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("e_Mekteb.Models.VjerouciteljUcenik", b =>
                {
                    b.HasOne("e_Mekteb.Models.AplicationUser", "Ucenik")
                        .WithMany()
                        .HasForeignKey("UcenikId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("e_Mekteb.Models.AplicationUser", "Vjeroucitelj")
                        .WithMany()
                        .HasForeignKey("VjerouciteljId")
                        .OnDelete(DeleteBehavior.NoAction);
                });
#pragma warning restore 612, 618
        }
    }
}
