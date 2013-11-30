using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportStore.Domain.Abstract;

namespace SportStore.Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
//        private SportsStoreEntities _context = new SportsStoreEntities();
        private EFDbContext _context = new EFDbContext();

        public IQueryable<Product> Products
        {
            get { return _context.Products; }
        }

    }
}
