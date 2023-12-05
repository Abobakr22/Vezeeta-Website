using Core.Consts;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class DiscountCouponDto
    {
        public int Id {  get; set; }
        public string Code { get; set; }
        public DiscountType DiscountType { get; set; }
        public Double Value { get; set; }
        public int CompletedRequests { get; set; }

         
 	
     //Update => {id,discoundCode,#requests,discoundType(enum),value	
     //Add => {discoundCode,#requests(Completed),discoundType(enum),value

    }
}

