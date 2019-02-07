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
         
        }
    }
}