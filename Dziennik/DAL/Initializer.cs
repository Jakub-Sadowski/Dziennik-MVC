using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Dziennik.Models;
using System.Diagnostics;

namespace Dziennik.DAL
{
    public class Initializer : System.Data.Entity.CreateDatabaseIfNotExists<Context>
    {
        protected override void Seed(Context context)
        {
            Debug.WriteLine("janek");
            var uczniowie = new List<Uczen>
            {
            new Uczen{imie = "Jan", nazwisko = "Kucharski", login = "kucharz96", haslo="1234"},
            new Uczen{imie = "Kamil", nazwisko = "Jarmoc", login = "elkamilaszczy", haslo="1234" },
            new Uczen{imie = "Marcin", nazwisko = "Krasucki", login = "rafonix", haslo="1234" },
            };

            uczniowie.ForEach(s => context.Uczniowie.Add(s));
            context.SaveChanges();
        }
    }
}