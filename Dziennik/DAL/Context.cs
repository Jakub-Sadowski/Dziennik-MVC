using Dziennik.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Dziennik.DAL
{
    public class Context : DbContext
    {

        public Context() : base("Context")
        {

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

            
            modelBuilder.Entity<Uczen>()
           .HasOptional(o => o.Klasa)
           .WithMany(m => m.Uczniowie)
           .HasForeignKey(k => k.KlasaID)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<Lekcja>()
           .HasOptional(o => o.Klasa)
           .WithMany(m => m.Lekcje)
           .HasForeignKey(k => k.KlasaID)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<Plik>()
           .HasOptional(o => o.Klasa)
           .WithMany(m => m.Pliki)
           .HasForeignKey(k => k.KlasaID)
           .WillCascadeOnDelete(true);

            modelBuilder.Entity<Test>()
          .HasOptional(o => o.Klasa)
          .WithMany(m => m.Testy)
          .HasForeignKey(k => k.KlasaID)
          .WillCascadeOnDelete(true);

            modelBuilder.Entity<Spoznienie>()
         .HasOptional(o => o.Lekcja)
         .WithMany(m => m.Spoznienia)
         .HasForeignKey(k => k.LekcjaID)
         .WillCascadeOnDelete(false);

              modelBuilder.Entity<Nieobecnosc>()
         .HasOptional(o => o.Lekcja)
         .WithMany(m => m.Nieobecnosci)
         .HasForeignKey(k => k.LekcjaID)
         .WillCascadeOnDelete(false);

                modelBuilder.Entity<Klasa>()
         .HasOptional(o => o.Wychowawca)
         .WithMany(m => m.WychowywaneKlasy)
         .HasForeignKey(k => k.WychowawcaID)
         .WillCascadeOnDelete(false);

                modelBuilder.Entity<Lekcja>()
         .HasOptional(o => o.Nauczyciel)
         .WithMany(m => m.Lekcje)
         .HasForeignKey(k => k.NauczycielID)
         .WillCascadeOnDelete(false);
        

               modelBuilder.Entity<Uwaga>()
         .HasOptional(o => o.Nauczyciel)
         .WithMany(m => m.Uwagi)
         .HasForeignKey(k => k.NauczycielID)
         .WillCascadeOnDelete(false);

               modelBuilder.Entity<Plik>()
         .HasOptional(o => o.Nauczyciel)
         .WithMany(m => m.Pliki)
         .HasForeignKey(k => k.NauczycielID)
         .WillCascadeOnDelete(false);

                  modelBuilder.Entity<Test>()
         .HasOptional(o => o.Nauczyciel)
         .WithMany(m => m.Testy)
         .HasForeignKey(k => k.NauczycielID)
         .WillCascadeOnDelete(false);

                  modelBuilder.Entity<Ogloszenie>()
         .HasOptional(o => o.Nauczyciel)
         .WithMany(m => m.Ogloszenia)
         .HasForeignKey(k => k.NauczycielID)
         .WillCascadeOnDelete(false);

                  modelBuilder.Entity<Ogloszenie_dla_rodzicow>()
         .HasOptional(o => o.Nauczyciel)
         .WithMany(m => m.Ogloszenia_r)
         .HasForeignKey(k => k.NauczycielID)
         .WillCascadeOnDelete(false);

                  modelBuilder.Entity<Ocena>()
         .HasOptional(o => o.Nauczyciel)
         .WithMany(m => m.Oceny)
         .HasForeignKey(k => k.NauczycielID)
         .WillCascadeOnDelete(false);

                    modelBuilder.Entity<Ocena>()
         .HasOptional(o => o.Przedmiot)
         .WithMany(m => m.Oceny)
         .HasForeignKey(k => k.PrzedmiotID)
         .WillCascadeOnDelete(true);

                      modelBuilder.Entity<Lekcja>()
         .HasOptional(o => o.Przedmiot)
         .WithMany(m => m.Lekcje)
         .HasForeignKey(k => k.PrzedmiotID)
         .WillCascadeOnDelete(true);

                      modelBuilder.Entity<Plik>()
         .HasOptional(o => o.Przedmiot)
         .WithMany(m => m.Pliki)
         .HasForeignKey(k => k.PrzedmiotID)
         .WillCascadeOnDelete(true);

                         modelBuilder.Entity<Test>()
         .HasOptional(o => o.Przedmiot)
         .WithMany(m => m.Testy)
         .HasForeignKey(k => k.PrzedmiotID)
         .WillCascadeOnDelete(true);

                            modelBuilder.Entity<Ogloszenie_dla_rodzicow>()
         .HasOptional(o => o.Rodzic)
         .WithMany(m => m.Ogloszenia)
         .HasForeignKey(k => k.RodzicID)
         .WillCascadeOnDelete(true);

              modelBuilder.Entity<Uczen>()
.HasOptional(o => o.Rodzic)
.WithMany(m => m.Uczniowie)
.HasForeignKey(k => k.RodzicID)
.WillCascadeOnDelete(true);
         

            modelBuilder.Entity<Testy_ucznia>()
         .HasOptional(o => o.Test)
         .WithMany(m => m.Testy)
         .HasForeignKey(k => k.TestID)
         .WillCascadeOnDelete(false);

                                    modelBuilder.Entity<Pytanie>()
         .HasOptional(o => o.Test)
         .WithMany(m => m.Pytania)
         .HasForeignKey(k => k.TestID)
         .WillCascadeOnDelete(true);


                                       modelBuilder.Entity<Ocena>()
         .HasOptional(o => o.Uczen)
         .WithMany(m => m.Oceny)
         .HasForeignKey(k => k.UczenID)
         .WillCascadeOnDelete(true);
    
                                          modelBuilder.Entity<Uwaga>()
         .HasOptional(o => o.Uczen)
         .WithMany(m => m.Uwagi)
         .HasForeignKey(k => k.UczenID)
         .WillCascadeOnDelete(true);


                                            modelBuilder.Entity<Spoznienie>()
         .HasOptional(o => o.Uczen)
         .WithMany(m => m.Spoznienia)
         .HasForeignKey(k => k.UczenID)
         .WillCascadeOnDelete(true);

                                               modelBuilder.Entity<Nieobecnosc>()
         .HasOptional(o => o.Uczen)
         .WithMany(m => m.Nieobecnosci)
         .HasForeignKey(k => k.UczenID)
         .WillCascadeOnDelete(true);

          modelBuilder.Entity<Testy_ucznia>()
         .HasOptional(o => o.Uczen)
         .WithMany(m => m.Testy)
         .HasForeignKey(k => k.UczenID)
         .WillCascadeOnDelete(true);
        }


    }


}
