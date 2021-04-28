using e_Mekteb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_Mekteb.ViewModel;

namespace e_Mekteb.ApDbContext
{
    public class e_MektebDbContext:IdentityDbContext<AplicationUser>
    {
        public e_MektebDbContext(DbContextOptions<e_MektebDbContext> options) : base(options)
        {

        }

        public DbSet<Aktivnost> Aktivnosti { get; set; }
        public DbSet<Medzlis> Medzlisi { get; set; }
        public DbSet<Adresa> Adrese { get; set; }
        public DbSet<SkolskaGodina> SkolskeGodine { get; set; }
        public DbSet<Razred> Razredi { get; set; }
        public DbSet<ClanMualimskogVijeca> ClanoviMualimskogVijeca { get; set; }
        public DbSet<Biljeska> Biljeske { get; set; }
        public DbSet<Prisutnost> Prisutnosti { get; set; }

        public DbSet<Skola> Skole { get; set; }

        public DbSet<UcenikAktivnost> Pohada { get; set; }
        public DbSet<VjerouciteljAktivnost> Predaje { get; set; }
        public DbSet<VjerouciteljUcenik> VjerouciteljUcenik { get; set; }
        public DbSet<Obavijest> Obavijesti { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UcenikAktivnost>().HasKey(t => new { t.AplicationUserId, t.AktivnostId });
            //modelBuilder.Entity<VjerouciteljAktivnost>().HasKey(t => new { t.AplicationUserId, t.AktivnostId });
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.NoAction;
            }
            










        }
    }
}
