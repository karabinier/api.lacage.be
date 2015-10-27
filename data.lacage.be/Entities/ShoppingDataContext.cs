using common.lacage.be.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.lacage.be.Entities
{
    public class ShoppingDataContext:DbContext
    {
        public ShoppingDataContext()
            : base("DefaultConnection")
        {

        }

        public DbSet<ListItem> ListItems { get; set; }
    }
}
