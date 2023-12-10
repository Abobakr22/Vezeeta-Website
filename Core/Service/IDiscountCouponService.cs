using Core.Dtos;
using Core.Models;
using Core.Repository;

namespace Core.Service
{
    public interface IDiscountCouponService : IBaseRepository<DiscountCoupon>
    {
        Task<bool> AddDiscountCoupon(DiscountCouponDto DiscountCouponDto);
        Task<bool> UpdateDiscountCoupon(DiscountCouponDto UpdatedCoupon);
        Task<bool> DeleteDiscountCoupon(int CouponId);
        Task<bool> DeactivateDiscountCoupon(int CouponId);
    }
}