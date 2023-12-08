using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class DiscountCouponId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_DiscountCoupons_DiscountCouponId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DiscountCouponId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DiscountCouponId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "DiscountCouponId",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_DiscountCouponId",
                table: "Bookings",
                column: "DiscountCouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_DiscountCoupons_DiscountCouponId",
                table: "Bookings",
                column: "DiscountCouponId",
                principalTable: "DiscountCoupons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_DiscountCoupons_DiscountCouponId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_DiscountCouponId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "DiscountCouponId",
                table: "Bookings");

            migrationBuilder.AddColumn<int>(
                name: "DiscountCouponId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DiscountCouponId",
                table: "AspNetUsers",
                column: "DiscountCouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_DiscountCoupons_DiscountCouponId",
                table: "AspNetUsers",
                column: "DiscountCouponId",
                principalTable: "DiscountCoupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
