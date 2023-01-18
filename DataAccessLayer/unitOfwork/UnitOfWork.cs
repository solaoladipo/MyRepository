using DataAccessLayer.GenericRepository;
using DTOS.AppModel;
using DTOS.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.unitOfwork
{
    public class UnitOfWork
    {
        private AppContext context;

        public UnitOfWork(AppContext context) 
        { 
            this.context = context;
            Beneficiary = new BeneficiaryRepository(this.context);
            CodingHead = new CodingHeadRepository(this.context);
            CodingDetail = new CodingDetailRepository(this.context);
        }

        public IBeneficiaryRepository Beneficiary { get; private set; }
        public ICodingHeadRepository CodingHead { get; private set; }
        public ICodingDetailRepository CodingDetail { get; private set; }
        public int save()
        {
           return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

    }
}
