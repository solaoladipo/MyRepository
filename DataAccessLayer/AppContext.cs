using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class AppContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=NGSL-VI-0531\SANLAMSERVER;Database=ArcelormittaWebApiDB;User Id=sa;password=sqluser0712$;Trusted_Connection=False;MultipleActiveResultSets=true;");
        }
    }
}
