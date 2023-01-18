using DTOS.AppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        IBeneficiaryRepository Beneficiary { get; }
        ICodingHeadRepository CodingHead { get; }
        ICodingDetailRepository CodingDetail { get; }
        int Save();
    }
}
