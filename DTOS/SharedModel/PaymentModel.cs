using DTOS.AppModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS.SharedModel
{
    public class PaymentModel
    {
        public PaymentModel()
        {
            this.CodingDetail = new List<CodingDetail>();
        }

        public System.Guid ColdingHeadId { get; set; }
        public string? Refno { get; set; }
        public string? BeneficiaryCode { get; set; }
        public System.DateTime TransDate { get; set; }
        public string? Documentno { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? VAT { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? WHT { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public bool sendforapproval { get; set; }
        public bool approved { get; set; }
        public string? createdby { get; set; }
        public System.DateTime Datecreated { get; set; }
        public string? currency { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? EarlierAdditionorDeduction { get; set; }
        public string? AnotherCharges { get; set; }

        public  IEnumerable<CodingDetail>? CodingDetail { get; set; }
    }
}
