namespace Poc.Services.CouponAPI.Models
{
    public class Coupon: CouponBase
    {       
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }

    public class CouponBase
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; }
    }
}
