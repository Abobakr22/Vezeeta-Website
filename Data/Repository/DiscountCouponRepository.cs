using Core.Dtos;
using Core.Models;
using Core.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class DiscountCouponRepository : BaseRepository<DiscountCoupon>, IDiscountCouponRepository
    {
        private readonly ApplicationDbContext _context;
        //private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public DiscountCouponRepository(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager,
              UserManager<ApplicationUser> userManager) : base(context, signInManager, userManager)
        {
            _context = context;
            //  _signInManager = signInManager;
            _userManager = userManager;
        }



        //{discoundCode,#requests(Completed),discoundType(enum),value  }
        public async Task<bool> AddDiscountCoupon(DiscountCouponDto DiscountCouponDto)
        {
            var NewCoupon = new DiscountCoupon()
            {
                DiscountCode = DiscountCouponDto.Code,
                CompletedRequests = DiscountCouponDto.CompletedRequests,
                DiscountType = DiscountCouponDto.DiscountType,
                DiscountAmount = DiscountCouponDto.Value,
                ExpirationDate = DiscountCouponDto.ExpirationDate,
                IsValid = DiscountCouponDto.IsValid
            };
            await _context.DiscountCoupons.AddAsync(NewCoupon);
            await _context.SaveChangesAsync();
            return true;
        }

        //{id,discoundCode,#requests,discoundType(enum),value }
        public async Task<bool> UpdateDiscountCoupon(DiscountCouponDto UpdatedCoupon)
        {
            var ExistedCoupon = await _context.DiscountCoupons.FirstOrDefaultAsync(x => x.Id == UpdatedCoupon.Id);
            if (ExistedCoupon is not null)
            {
                // Update existing coupon properties
                ExistedCoupon.DiscountCode = UpdatedCoupon.Code;
                ExistedCoupon.CompletedRequests = UpdatedCoupon.CompletedRequests;
                ExistedCoupon.DiscountType = UpdatedCoupon.DiscountType;
                ExistedCoupon.ExpirationDate = UpdatedCoupon.ExpirationDate;
                ExistedCoupon.DiscountAmount = UpdatedCoupon.Value;

                //var result = _context.DiscountCoupons.Update(ExistedCoupon);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeactivateDiscountCoupon(int CouponId)
        {
            var coupon= await _context.DiscountCoupons.FindAsync(CouponId);
            if(coupon is not null && coupon.IsValid )
            {
                    coupon.IsValid = false;
                    await _context.SaveChangesAsync();
                    return true; 
            }
            return false;
        }

        public async Task<bool> DeleteDiscountCoupon(int CouponId)
        {
            var coupon = await _context.DiscountCoupons.FirstOrDefaultAsync(x => x.Id == CouponId);
            if (coupon is not null)
            {
                bool isExist = _context.DiscountCoupons.Any(x => x.Id == CouponId);
                if (isExist)
                {
                   var result = _context.DiscountCoupons.Remove(coupon);
                   await _context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
        }

    }
}
