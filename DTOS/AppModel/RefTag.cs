using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOS.AppModel
{
    public class RefTag
    {
        [Key]
        public Guid RefnoID { get; set; }
        public string? DocumentName { get; set;}
        public string? TagName { get; set; }
        public int ? serialNo { get; set; }
    }
}
