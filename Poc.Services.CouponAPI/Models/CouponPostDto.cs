using System.Text.Json.Serialization;

namespace Poc.Services.CouponAPI.Models
{
    public class CouponPostDto
    {        
        [JsonRequired]
        public string CouponCode { get; set; }
        [JsonRequired]
        public double DiscountAmount { get; set; }
        [JsonRequired]
        public int MinAmount { get; set; }
    }
}
