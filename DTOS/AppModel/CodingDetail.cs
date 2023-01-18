using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS.AppModel
{
    public class CodingDetail
    {
        [Key]
        public System.Guid ColdingDetailsId { get; set; }
        public System.Guid ColdingHeadId { get; set; }
        public string? Particulars { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? ActualAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TransactionFee { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? WHT { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? VAT { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? NetAmount { get; set; }
        public string? Allocate { get; set; }

        public virtual CodingHead? CodingHead { get; set; }


    }
}
