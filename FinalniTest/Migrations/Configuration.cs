namespace FinalniTest.Migrations
{
    using FinalniTest.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FinalniTest.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FinalniTest.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.OrganizacioneJedinice.AddOrUpdate(x => x.Id,
                new OrganizacionaJedinica() { Id = 1, Ime = "Administracija", GodinaOsnivanja = 2010},
                new OrganizacionaJedinica() { Id = 2, Ime = "Racunovodstvo", GodinaOsnivanja = 2012},
                new OrganizacionaJedinica() { Id = 3, Ime = "Razvoj", GodinaOsnivanja = 2013}
                );

            context.Zaposleni.AddOrUpdate(x => x.Id,
                new Zaposleni() { Id = 1, ImeIPrezime = "Pera Peric", Rola = "Direktor", GodinaRodjenja = 1980, GodinaZaposlenja = 2010, Plata = 3000, JedinicaId = 1},
                new Zaposleni() { Id = 2, ImeIPrezime = "Mika Mikic", Rola = "Sekretar", GodinaRodjenja = 1985, GodinaZaposlenja = 2011, Plata = 1000, JedinicaId = 1},
                new Zaposleni() { Id = 3, ImeIPrezime = "Iva Ivic", Rola = "Racunovocja", GodinaRodjenja = 1981, GodinaZaposlenja = 2012, Plata = 2000, JedinicaId = 2},
                new Zaposleni() { Id = 4, ImeIPrezime = "Zika Zikic", Rola = "Inzenjer", GodinaRodjenja = 1982, GodinaZaposlenja = 2013, Plata = 2500, JedinicaId = 3},
                new Zaposleni() { Id = 5, ImeIPrezime = "Ana Anic", Rola = "Inzenjer", GodinaRodjenja = 1984, GodinaZaposlenja = 2014, Plata = 2500, JedinicaId = 3}
                );
        }
    }
}
