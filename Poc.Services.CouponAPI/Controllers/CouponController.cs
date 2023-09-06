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
        private ResponseDto _response { get; set; }

        public CouponController(AppDbContext db)
        {
            _db = db;
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons.ToList();
                _response.Result = objList;
                _response.TotalCount = objList.Count();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _response.TotalCount = 0;
            }
            return _response;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon objList = _db.Coupons.First(u => u.CouponId == id);
                _response.Result = objList;
                _response.TotalCount = objList is not null ? 1 : 0;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _response.TotalCount = 0;
            }
            return _response;
        }

        [HttpGet]
        [Route("{code}")]
        public ResponseDto Get(string code)
        {
            try
            {
                IEnumerable<Coupon> objList = _db.Coupons
                                                .Where(c => c.CouponCode.Contains(code)).ToList();
                _response.Result = objList;
                _response.TotalCount = objList.Count();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _response.TotalCount = 0;
            }
            return _response;
        }

    }
}
