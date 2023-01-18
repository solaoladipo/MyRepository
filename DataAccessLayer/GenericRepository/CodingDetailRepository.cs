using DTOS.AppModel;
using DTOS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.GenericRepository
{
    public class CodingDetailRepository : GenericRepository<CodingDetail>, ICodingDetailRepository
    {
        public CodingDetailRepository(AppContext context) : base(context) { }
    }
}
