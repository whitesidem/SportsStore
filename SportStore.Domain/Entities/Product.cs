using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SportStore.Domain.Concrete
{

    public partial class Product
    {
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public global::System.Int32 ProductID { get; set; }
        public global::System.String Name { get; set; }
        public global::System.String Description { get; set; }
        public global::System.String Category { get; set; }
        public global::System.Decimal Price { get; set; }
    }

}
