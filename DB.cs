using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace _254008_L02
{
    public class DB : DbContext
    {
        public virtual DbSet<Serial> Serialized { get; set; }
    }
}
