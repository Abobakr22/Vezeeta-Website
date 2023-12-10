using Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public class DiscountCoupon
    {
        public int Id { get; set; }
        public string DiscountCode { get; set; }
        public int CompletedRequests { get; set; }         //number of completed requests
        public DiscountType DiscountType { get; set; }
        public bool IsValid { get; set; }
        public double DiscountAmount { get; set; }         //Value of discount

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }
    }
}