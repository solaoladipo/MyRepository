using DTOS.AppModel;
using DTOS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.GenericRepository
{
    public class CodingHeadRepository : GenericRepository<CodingHead>, ICodingHeadRepository
    {
        public CodingHeadRepository(AppContext context) : base(context) { }
    }
}
