using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace SportStore.Domain.Concrete
{
    public class EFDbContext :DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}
