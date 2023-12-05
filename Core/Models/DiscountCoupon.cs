using Core.Consts;


namespace Core.Models
{
    public class DiscountCoupon
    {
        public int Id { get; set; }
        public string DiscountCode { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CompletedRequests { get; set; }

        //number of completed requests
        public DiscountType DiscountType { get; set; }
        public bool IsValid { get; set; }

        //Value of discount
        public double DiscountAmount { get; set; }

    }
}
