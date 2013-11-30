using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportStore.Domain.Concrete;

namespace SportStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }

}
