namespace Poc.Services.CouponAPI.Models
{
    public class CouponDto: CouponBase
    {
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
