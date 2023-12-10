using Core.Dtos;
using Core.Models;
using Core.Repository;
using Core.Service;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services
{
    public class DiscountCouponService : BaseRepository<DiscountCoupon>, IDiscountCouponService
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public DiscountCouponService(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager,
              UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base(context, signInManager, userManager, roleManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //{discoundCode,#requests(Completed),discoundType(enum),value }
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

        //{id,discoundCode,#requests,discoundType(enum),value}
        public async Task<bool> UpdateDiscountCoupon(DiscountCouponDto UpdatedCoupon)
        {
            var ExistedCoupon = await _context.DiscountCoupons.FirstOrDefaultAsync(x => x.Id == UpdatedCoupon.Id);
            if (ExistedCoupon is not null)
            {
                ExistedCoupon.DiscountCode = UpdatedCoupon.Code;
                ExistedCoupon.CompletedRequests = UpdatedCoupon.CompletedRequests;
                ExistedCoupon.DiscountType = UpdatedCoupon.DiscountType;
                ExistedCoupon.ExpirationDate = UpdatedCoupon.ExpirationDate;
                ExistedCoupon.DiscountAmount = UpdatedCoupon.Value;

                _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> DeactivateDiscountCoupon(int CouponId)
        {
            var coupon = await _context.DiscountCoupons.FindAsync(CouponId);
            if (coupon is not null && coupon.IsValid)
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

        public Task<DiscountCoupon> FindAsync(Expression<Func<DiscountCoupon, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}