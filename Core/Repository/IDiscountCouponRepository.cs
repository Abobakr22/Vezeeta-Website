using Core.Dtos;
using Core.Models;

namespace Core.Repository
{
    public interface IDiscountCouponRepository : IBaseRepository<DiscountCoupon>
    {
        Task<bool> AddDiscountCoupon(DiscountCouponDto DiscountCouponDto);
        Task<bool> UpdateDiscountCoupon(DiscountCouponDto UpdatedCoupon);
        Task<bool> DeleteDiscountCoupon(int CouponId);
        Task<bool> DeactivateDiscountCoupon(int CouponId);
    }
}