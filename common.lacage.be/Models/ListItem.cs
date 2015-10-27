using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace common.lacage.be.Models
{
    public class ListItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool Listed { get; set; }
        public bool Bought { get; set; }
        public DateTime BoughtOn { get; set; }
        public string Comment { get; set; }        
    }
}
