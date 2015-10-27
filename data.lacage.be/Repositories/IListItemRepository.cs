using common.lacage.be.Models;
using data.lacage.be.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace data.lacage.be.Repositories
{
    public interface IListItemRepository
    {
        IQueryable<ListItem> GetListItems();
    }
}
