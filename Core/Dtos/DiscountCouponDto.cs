using Core.Consts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class DiscountCouponDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public DiscountType DiscountType { get; set; }
        public Double Value { get; set; }
        public int CompletedRequests { get; set; }
        public bool IsValid { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
         public DateTime ExpirationDate { get; set; }


    }
}

