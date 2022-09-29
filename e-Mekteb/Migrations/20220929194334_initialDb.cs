using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace e_Mekteb.Migrations
{
    public partial class initialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adrese",
                columns: table => new
                {
                    AdresaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NazivMjesta = table.Column<string>(maxLength: 50, nullable: false),
                    Ulica = table.Column<string>(maxLength: 100, nullable: false),
                    PostanskiBroj = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adrese", x => x.AdresaId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SkolskeGodine",
                columns: table => new
                {
                    SkolskaGodinaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Godina = table.Column<string>(maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkolskeGodine", x => x.SkolskaGodinaId);
                });

            migrationBuilder.CreateTable(
                name: "Medzlisi",
                columns: table => new
                {
                    MedzlisId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdresaId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 100, nullable: false),
                    Kontakt = table.Column<string>(maxLength: 12, nullable: false),
                    GlavniImam = table.Column<string>(maxLength: 50, nullable: false),
                    EmailGlavnogImama = table.Column<string>(maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medzlisi", x => x.MedzlisId);
                    table.ForeignKey(
                        name: "FK_Medzlisi_Adrese_AdresaId",
                        column: x => x.AdresaId,
                        principalTable: "Adrese",
                        principalColumn: "AdresaId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Aktivnosti",
                columns: table => new
                {
                    AktivnostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkolskaGodinaId = table.Column<int>(nullable: false),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    TipAktivnosti = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aktivnosti", x => x.AktivnostId);
                    table.ForeignKey(
                        name: "FK_Aktivnosti_SkolskeGodine_SkolskaGodinaId",
                        column: x => x.SkolskaGodinaId,
                        principalTable: "SkolskeGodine",
                        principalColumn: "SkolskaGodinaId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    AplicationUserId = table.Column<string>(nullable: true),
                    ImeiPrezime = table.Column<string>(maxLength: 50, nullable: true),
                    Spol = table.Column<int>(nullable: false),
                    NazivMjesta = table.Column<string>(maxLength: 50, nullable: false),
                    Ulica = table.Column<string>(maxLength: 100, nullable: true),
                    PostanskiBroj = table.Column<string>(maxLength: 5, nullable: true),
                    DatumRodenja = table.Column<DateTime>(nullable: false),
                    ImeiPrezimeRoditelja = table.Column<string>(maxLength: 50, nullable: true),
                    BrojMobitela = table.Column<string>(nullable: true),
                    MedzlisId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_AplicationUserId",
                        column: x => x.AplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Medzlisi_MedzlisId",
                        column: x => x.MedzlisId,
                        principalTable: "Medzlisi",
                        principalColumn: "MedzlisId");
                });

            migrationBuilder.CreateTable(
                name: "ClanoviMualimskogVijeca",
                columns: table => new
                {
                    ClanMualimskogVijecaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedzlisId = table.Column<int>(nullable: false),
                    ImeIPrezimeClanaVijeca = table.Column<string>(maxLength: 50, nullable: false),
                    EmailClanaVijeca = table.Column<string>(nullable: false),
                    KontaktClanaVijeca = table.Column<string>(maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClanoviMualimskogVijeca", x => x.ClanMualimskogVijecaId);
                    table.ForeignKey(
                        name: "FK_ClanoviMualimskogVijeca_Medzlisi_MedzlisId",
                        column: x => x.MedzlisId,
                        principalTable: "Medzlisi",
                        principalColumn: "MedzlisId");
                });

            migrationBuilder.CreateTable(
                name: "Razredi",
                columns: table => new
                {
                    RazredId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naziv = table.Column<string>(maxLength: 50, nullable: false),
                    MedzlisId = table.Column<int>(nullable: true),
                    SkolskaGodinaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Razredi", x => x.RazredId);
                    table.ForeignKey(
                        name: "FK_Razredi_Medzlisi_MedzlisId",
                        column: x => x.MedzlisId,
                        principalTable: "Medzlisi",
                        principalColumn: "MedzlisId");
                    table.ForeignKey(
                        name: "FK_Razredi_SkolskeGodine_SkolskaGodinaId",
                        column: x => x.SkolskaGodinaId,
                        principalTable: "SkolskeGodine",
                        principalColumn: "SkolskaGodinaId");
                });

            migrationBuilder.CreateTable(
                name: "Skole",
                columns: table => new
                {
                    SkolaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedzlisId = table.Column<int>(nullable: false),
                    VjerouciteljId = table.Column<string>(nullable: true),
                    NazivSkole = table.Column<string>(nullable: true),
                    Grad = table.Column<string>(nullable: true),
                    Adresa = table.Column<string>(nullable: true),
                    PostanskiBroj = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skole", x => x.SkolaId);
                    table.ForeignKey(
                        name: "FK_Skole_Medzlisi_MedzlisId",
                        column: x => x.MedzlisId,
                        principalTable: "Medzlisi",
                        principalColumn: "MedzlisId");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Biljeske",
                columns: table => new
                {
                    BiljeskaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(nullable: false),
                    AplicationUserId = table.Column<string>(nullable: true),
                    AktivnostId = table.Column<int>(nullable: false),
                    Biljeske = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Biljeske", x => x.BiljeskaId);
                    table.ForeignKey(
                        name: "FK_Biljeske_Aktivnosti_AktivnostId",
                        column: x => x.AktivnostId,
                        principalTable: "Aktivnosti",
                        principalColumn: "AktivnostId");
                    table.ForeignKey(
                        name: "FK_Biljeske_AspNetUsers_AplicationUserId",
                        column: x => x.AplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Obavijesti",
                columns: table => new
                {
                    ObavijestId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(nullable: false),
                    Naslov = table.Column<string>(nullable: false),
                    Sadrzaj = table.Column<string>(maxLength: 500, nullable: false),
                    VjerouciteljId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obavijesti", x => x.ObavijestId);
                    table.ForeignKey(
                        name: "FK_Obavijesti_AspNetUsers_VjerouciteljId",
                        column: x => x.VjerouciteljId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pohada",
                columns: table => new
                {
                    AktivnostId = table.Column<int>(nullable: false),
                    UcenikId = table.Column<string>(nullable: false),
                    UcenikAktivnostId = table.Column<int>(nullable: false),
                    VjerouciteljId = table.Column<string>(nullable: true),
                    NazivPredmeta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pohada", x => new { x.UcenikId, x.AktivnostId });
                    table.ForeignKey(
                        name: "FK_Pohada_Aktivnosti_AktivnostId",
                        column: x => x.AktivnostId,
                        principalTable: "Aktivnosti",
                        principalColumn: "AktivnostId");
                    table.ForeignKey(
                        name: "FK_Pohada_AspNetUsers_UcenikId",
                        column: x => x.UcenikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Predaje",
                columns: table => new
                {
                    AktivnostId = table.Column<int>(nullable: false),
                    VjerouciteljId = table.Column<string>(nullable: false),
                    VjerouciteljAktivnostId = table.Column<int>(nullable: false),
                    NazivPredmeta = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predaje", x => new { x.VjerouciteljId, x.AktivnostId });
                    table.ForeignKey(
                        name: "FK_Predaje_Aktivnosti_AktivnostId",
                        column: x => x.AktivnostId,
                        principalTable: "Aktivnosti",
                        principalColumn: "AktivnostId");
                    table.ForeignKey(
                        name: "FK_Predaje_AspNetUsers_VjerouciteljId",
                        column: x => x.VjerouciteljId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Prisutnosti",
                columns: table => new
                {
                    PrisutnostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Datum = table.Column<DateTime>(nullable: false),
                    AplicationUserId = table.Column<string>(nullable: true),
                    AktivnostId = table.Column<int>(nullable: false),
                    IsPrisutan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prisutnosti", x => x.PrisutnostId);
                    table.ForeignKey(
                        name: "FK_Prisutnosti_Aktivnosti_AktivnostId",
                        column: x => x.AktivnostId,
                        principalTable: "Aktivnosti",
                        principalColumn: "AktivnostId");
                    table.ForeignKey(
                        name: "FK_Prisutnosti_AspNetUsers_AplicationUserId",
                        column: x => x.AplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "VjerouciteljUcenik",
                columns: table => new
                {
                    VjerouciteljUcenikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UcenikId = table.Column<string>(nullable: true),
                    VjerouciteljId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VjerouciteljUcenik", x => x.VjerouciteljUcenikId);
                    table.ForeignKey(
                        name: "FK_VjerouciteljUcenik_AspNetUsers_UcenikId",
                        column: x => x.UcenikId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VjerouciteljUcenik_AspNetUsers_VjerouciteljId",
                        column: x => x.VjerouciteljId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RazrediUcenik",
                columns: table => new
                {
                    RazredUcenikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Razred = table.Column<string>(nullable: true),
                    VjerouciteljId = table.Column<string>(nullable: true),
                    UcenikId = table.Column<string>(nullable: true),
                    MedzlisId = table.Column<int>(nullable: false),
                    SkolaId = table.Column<int>(nullable: false),
                    SkolskaGodinaId = table.Column<int>(nullable: false),
                    DatumUpisa = table.Column<DateTime>(nullable: false),
                    DatumIspisa = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RazrediUcenik", x => x.RazredUcenikId);
                    table.ForeignKey(
                        name: "FK_RazrediUcenik_Skole_SkolaId",
                        column: x => x.SkolaId,
                        principalTable: "Skole",
                        principalColumn: "SkolaId");
                    table.ForeignKey(
                        name: "FK_RazrediUcenik_SkolskeGodine_SkolskaGodinaId",
                        column: x => x.SkolskaGodinaId,
                        principalTable: "SkolskeGodine",
                        principalColumn: "SkolskaGodinaId");
                });

            migrationBuilder.CreateTable(
                name: "SkoleUcenika",
                columns: table => new
                {
                    SkolaUcenikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UcenikId = table.Column<string>(nullable: true),
                    NazivSkole = table.Column<string>(nullable: true),
                    VjerouciteljId = table.Column<string>(nullable: true),
                    SkolaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkoleUcenika", x => x.SkolaUcenikId);
                    table.ForeignKey(
                        name: "FK_SkoleUcenika_Skole_SkolaId",
                        column: x => x.SkolaId,
                        principalTable: "Skole",
                        principalColumn: "SkolaId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aktivnosti_SkolskaGodinaId",
                table: "Aktivnosti",
                column: "SkolskaGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AplicationUserId",
                table: "AspNetUsers",
                column: "AplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_MedzlisId",
                table: "AspNetUsers",
                column: "MedzlisId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Biljeske_AktivnostId",
                table: "Biljeske",
                column: "AktivnostId");

            migrationBuilder.CreateIndex(
                name: "IX_Biljeske_AplicationUserId",
                table: "Biljeske",
                column: "AplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClanoviMualimskogVijeca_MedzlisId",
                table: "ClanoviMualimskogVijeca",
                column: "MedzlisId");

            migrationBuilder.CreateIndex(
                name: "IX_Medzlisi_AdresaId",
                table: "Medzlisi",
                column: "AdresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Obavijesti_VjerouciteljId",
                table: "Obavijesti",
                column: "VjerouciteljId");

            migrationBuilder.CreateIndex(
                name: "IX_Pohada_AktivnostId",
                table: "Pohada",
                column: "AktivnostId");

            migrationBuilder.CreateIndex(
                name: "IX_Predaje_AktivnostId",
                table: "Predaje",
                column: "AktivnostId");

            migrationBuilder.CreateIndex(
                name: "IX_Prisutnosti_AktivnostId",
                table: "Prisutnosti",
                column: "AktivnostId");

            migrationBuilder.CreateIndex(
                name: "IX_Prisutnosti_AplicationUserId",
                table: "Prisutnosti",
                column: "AplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Razredi_MedzlisId",
                table: "Razredi",
                column: "MedzlisId");

            migrationBuilder.CreateIndex(
                name: "IX_Razredi_SkolskaGodinaId",
                table: "Razredi",
                column: "SkolskaGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_RazrediUcenik_SkolaId",
                table: "RazrediUcenik",
                column: "SkolaId");

            migrationBuilder.CreateIndex(
                name: "IX_RazrediUcenik_SkolskaGodinaId",
                table: "RazrediUcenik",
                column: "SkolskaGodinaId");

            migrationBuilder.CreateIndex(
                name: "IX_Skole_MedzlisId",
                table: "Skole",
                column: "MedzlisId");

            migrationBuilder.CreateIndex(
                name: "IX_SkoleUcenika_SkolaId",
                table: "SkoleUcenika",
                column: "SkolaId");

            migrationBuilder.CreateIndex(
                name: "IX_VjerouciteljUcenik_UcenikId",
                table: "VjerouciteljUcenik",
                column: "UcenikId");

            migrationBuilder.CreateIndex(
                name: "IX_VjerouciteljUcenik_VjerouciteljId",
                table: "VjerouciteljUcenik",
                column: "VjerouciteljId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Biljeske");

            migrationBuilder.DropTable(
                name: "ClanoviMualimskogVijeca");

            migrationBuilder.DropTable(
                name: "Obavijesti");

            migrationBuilder.DropTable(
                name: "Pohada");

            migrationBuilder.DropTable(
                name: "Predaje");

            migrationBuilder.DropTable(
                name: "Prisutnosti");

            migrationBuilder.DropTable(
                name: "Razredi");

            migrationBuilder.DropTable(
                name: "RazrediUcenik");

            migrationBuilder.DropTable(
                name: "SkoleUcenika");

            migrationBuilder.DropTable(
                name: "VjerouciteljUcenik");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Aktivnosti");

            migrationBuilder.DropTable(
                name: "Skole");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "SkolskeGodine");

            migrationBuilder.DropTable(
                name: "Medzlisi");

            migrationBuilder.DropTable(
                name: "Adrese");
        }
    }
}
