using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context)
        {
            if (context.heros.Any()) return;
            
            var heroes = new List<Hero>
            {
               
            };

            await context.heros.AddRangeAsync(heroes);
            await context.SaveChangesAsync();
        }
    }
}