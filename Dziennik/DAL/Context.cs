using Dziennik.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Dziennik.DAL
{
    public class Context : DbContext
    {

        public Context() : base("Context")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Context, Dziennik.Migrations.Configuration>());

        }

        public DbSet<Administrator> Administratorzy { get; set; }
        public DbSet<Klasa> Klasy { get; set; }
        public DbSet<Lekcja> Lekcja { get; set; }

        public DbSet<Nauczyciel> Nauczyciele { get; set; }
        public DbSet<Nieobecnosc> Nieobecnosci { get; set; }

        public DbSet<Ocena> Oceny { get; set; }
        public DbSet<Ogloszenie> Ogloszenia { get; set; }
        public DbSet<Ogloszenie_dla_rodzicow> Ogloszenia_dla_rodzicow { get; set; }

        public DbSet<Plik> Pliki { get; set; }
        public DbSet<Przedmiot> Przedmioty { get; set; }
        public DbSet<Pytanie> Pytania { get; set; }

        public DbSet<Rodzic> Rodzice { get; set; }
        public DbSet<Spoznienie> Spoznienia { get; set; }

        public DbSet<Test> Testy { get; set; }
        public DbSet<Testy_ucznia> Testy_ucznia { get; set; }
        public DbSet<Tresc_ksztalcenia> Tresci_ksztalcenia { get; set; }

        public DbSet<Uczen> Uczniowie { get; set; }
        public DbSet<Uwaga> Uwagi { get; set; }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}