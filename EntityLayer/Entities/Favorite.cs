using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Entities
{
    public class Favorite
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int WorkId { get; set; }
        public virtual Work Work { get; set; }
    }
}
