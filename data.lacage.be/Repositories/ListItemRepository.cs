using common.lacage.be.Models;
using data.lacage.be.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.lacage.be.Repositories
{
    public class ListItemRepository:IListItemRepository
    {
        ShoppingDataContext _ctx = null;

        public ListItemRepository()
            {
                _ctx = new ShoppingDataContext();
            }

        public ListItemRepository(ShoppingDataContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<ListItem> GetListItems()
        {
            return _ctx.ListItems;
        }
    }
}
