using Core.Dtos;
using Core.Models;
using Core.Repository;
using Microsoft.AspNetCore.Identity;

namespace Data.Repository
{
    public class DiscountCouponRepository : BaseRepository<DiscountCoupon> , IDiscountCouponRepository
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
            };
            await _context.DiscountCoupons.AddAsync(NewCoupon);
            await _context.SaveChangesAsync();
            return true;
        }

        //{id,discoundCode,#requests,discoundType(enum),value }
    public async Task<bool> UpdateDiscountCoupon(DiscountCouponDto discountCouponDto )
        {
            if (discountCouponDto is not null)
            {
                var EditedCoupon = new DiscountCoupon()
                {
                    Id = discountCouponDto.Id,
                    DiscountCode = discountCouponDto.Code,
                    CompletedRequests = discountCouponDto.CompletedRequests,
                    DiscountType = discountCouponDto.DiscountType,
                    DiscountAmount = discountCouponDto.Value,

                };
                _context.DiscountCoupons.Update(EditedCoupon);
                await _context.SaveChangesAsync();
                return true;

            }
            return false;
        }

        public async Task<bool> DeactivateDiscountCoupon(int CouponId)
        {
            DiscountCoupon coupon= await _context.DiscountCoupons.FindAsync(CouponId);
            if(coupon is not null)
            {
               coupon.IsValid = false;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteDiscountCoupon(int CouponId)
        {
            DiscountCoupon coupon = await _context.DiscountCoupons.FindAsync(CouponId);
            if (coupon is not null)
            {
                bool isExist = _userManager.Users.Any(x => x.DiscountCouponId == CouponId);
                if (!isExist)
                {
                    _context.Remove(coupon);
                    return true;
                }
            }
            return false;
        }

    }
}
