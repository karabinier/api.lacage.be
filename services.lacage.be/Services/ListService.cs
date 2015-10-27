using common.lacage.be.Models;
using data.lacage.be.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace services.lacage.be.Services
{
    public class ListService
    {
        IListItemRepository _repo = null;

        public ListService() : this(new ListItemRepository()) { }


        public ListService(IListItemRepository repo)
        {
            _repo = repo;
        }

        public List<ListItem> GetAllListItems()
        {
            return _repo.GetListItems().ToList();
        }
    }
}
