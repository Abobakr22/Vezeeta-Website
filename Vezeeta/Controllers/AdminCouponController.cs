using Core.Dtos;
using Core.Dtos.DoctorDtos;
using Core.Repository;
using Data;
using Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminCouponController : ControllerBase
    {
        private readonly IDiscountCouponRepository _discountCouponRepository;
        private readonly ApplicationDbContext _context;
        public AdminCouponController(IDiscountCouponRepository discountCouponRepository, ApplicationDbContext context)
        {
            _discountCouponRepository = discountCouponRepository;
            _context = context;
        }


        [HttpPost("AddNewCoupon")]
        public async Task<IActionResult> AddDiscountCoupon([FromBody] DiscountCouponDto coupon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _discountCouponRepository.AddDiscountCoupon(coupon);
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while Adding a new Coupon : {ex.Message}");

            }

        }

        [HttpPatch("UpdateCoupon")]
        public async Task<IActionResult> UpdateDiscountCoupon([FromBody] DiscountCouponDto UpdatedCoupon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _discountCouponRepository.UpdateDiscountCoupon(UpdatedCoupon);
                    return Ok("Coupon Updated Successfully");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while Updating an Existing Coupon : {ex.Message}");

            }
        }

        [HttpPatch("DeactivateCoupon")]
        public async Task<IActionResult> DeactivateCoupon([FromQuery] int id)
        {
            var DeactivatedCoupon = await _discountCouponRepository.GetByIdAsync(id);
            if (DeactivatedCoupon is not null)
            {
                var result = await _discountCouponRepository.DeactivateDiscountCoupon(id);
                    return Ok(result);
                }
                return Ok("Coupon Not Found");
        }

        [HttpDelete("DeleteCoupon")]
        public async Task<IActionResult> DeleteCoupon([FromQuery] int id)
        {
            var DeletedCoupon = await _discountCouponRepository.GetByIdAsync(id);
            if (DeletedCoupon is not null)
            {
                var result = await _discountCouponRepository.DeleteDiscountCoupon(id);
                return Ok("DeletedCoupon Deleted Successfully");
            }
            return Ok("Coupon Not Exist ");
        }
        


    }
}
