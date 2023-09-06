using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private IMapper _mapper;

        public CouponController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        //[HttpGet]
        //public ResponseDto Get()
        //{
        //    try
        //    {
        //        IEnumerable<Coupon> objList = _db.Coupons.ToList();
        //        _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
        //        _response.TotalCount = objList.Count();
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.Message = ex.Message;
        //        _response.TotalCount = 0;
        //    }
        //    return _response;
        //}

        [HttpGet]
        [Route("{id:int}")]
        public ResponseDto Get(int id)
        {
            try
            {
                CouponModel obj = _db.Coupons.First(u => u.CouponId == id);
                _response.Result = _mapper.Map<CouponGetDto>(obj);
                _response.TotalCount = obj is not null ? 1 : 0;
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
        public ResponseDto Get(int take = 50, int skip = 0, string? code = "")
        {
            try
            {
                IEnumerable<CouponModel> objList = _db.Coupons
                                                .Where(c => c.CouponCode.Contains(code))
                                                .Take(take)
                                                .Skip(skip)
                                                .OrderBy(c => c.CouponCode)
                                                .ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponGetDto>>(objList);
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
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code, int take = 50, int skip = 0)
        {
            try
            {
                CouponModel obj = _db.Coupons.First(c => c.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponGetDto>(obj);
                _response.TotalCount = obj is not null ? 1 : 0;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                _response.TotalCount = 0;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto PostCoupon([FromBody] CouponPostDto couponPostDto)
        {
            try
            {
                CouponModel obj = _mapper.Map<CouponModel>(couponPostDto);
                _db.Coupons.Add(obj);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponGetDto>(obj);
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
