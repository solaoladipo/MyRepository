using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS.SharedModel
{
    public class Response
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
    }

    public class BeneficiaryModel
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
    }

}
