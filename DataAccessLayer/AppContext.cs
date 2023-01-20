using DTOS.AppModel;
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
        public AppContext(DbContextOptions options): base (options) { }
        public DbSet<Beneficiary>? Beneficiary { get; set; }
        public DbSet<CodingHead>? CodingHead { get; set; }
        public DbSet<CodingDetail>? CodingDetail { get; set; }
        public DbSet<RefTag>? RefTag { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=NGSL-VI-0531\SANLAMSERVER;Database=ArcelormittaWebApiDB;User Id=sa;password=sqluser0712$;Trusted_Connection=False;MultipleActiveResultSets=true;");
        //}
    }
}
