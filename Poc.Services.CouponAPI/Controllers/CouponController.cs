using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Poc.Services.CouponAPI.Data;
using Poc.Services.CouponAPI.Models;

namespace Poc.Services.CouponAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CouponController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public object Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();

                return objList;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [HttpGet]
        [Route("{id:int}")]
        public object Get(int id)
        {
            try
            {
                Coupon objList = _db.Coupons.First(u => u.CouponId == id);

                return objList;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        [HttpGet]
        [Route("{code}")]
        public object Get(string code)
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons
                                                .Where(c => c.CouponCode.Contains(code)).ToList();

                return objList;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

    }
}
