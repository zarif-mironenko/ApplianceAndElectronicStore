using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Infrastructure
{
    public abstract class DbInitializer<TDbContext> where TDbContext : DbContext
    {
        protected readonly TDbContext context;

        protected DbInitializer(TDbContext context)
        {
            this.context = context;
        }

        public abstract void SeedData();
    }
}
