using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SportStore.Domain.Concrete;

namespace SportStore.Domain.Entities
{
    /// <summary>
    /// Represents shopping basket
    /// </summary>
    public class Cart
    {
        //Represents lines in the basket - a distinct product is a distinct line
        private List<CartLine> _lineCollection = new List<CartLine>();

        public List<CartLine> Lines
        {
            get { return _lineCollection; }
        }

        public void AddItem(Product product, int quantity)
        {
            CartLine line = (_lineCollection
                .Where(p => p.Product.ProductID.Equals(product.ProductID)))
                .FirstOrDefault();

            if (line==null)
            {
                _lineCollection.Add(new CartLine(){ Product = product, Quantity = quantity});
            }
            else
            {
                line.Quantity += quantity;
            }

        }

        public void RemoveLine(Product product)
        {
            _lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return _lineCollection.Sum(p => p.Product.Price * p.Quantity);
        }

        public void Clear()
        {
            _lineCollection.Clear();
        }




    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

    }

}
