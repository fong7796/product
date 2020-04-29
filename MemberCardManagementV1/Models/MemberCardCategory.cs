using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MemberCardManagementV1.Models
{
    [Table("dbo.MemberCardCategory")]
    public class MemberCardCategory: BaseEntity
    {
        public long MemberCardCategoryID { get; set; }

        [Display(Name = "Tên hạng thẻ")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        [MaxLength(50, ErrorMessage = "Trường này tối đa 50 ký tự")]
        public string MemberCardCategoryName { get; set; }

        [Display(Name = "Doanh thu lên hạng (VND)")]
        [Range(0, 9999999999, ErrorMessage = "Trường này nằm trong khoảng 0 - 9.999.999.999")]
        public decimal? PromotionRevenue { get; set; }

        [Display(Name = "Thời hạn (ngày)")]
        public int? Duration { get; set; }

        [Display(Name = "Chiết khấu (%)")]
        [Range(0, 100, ErrorMessage = "Trường này nằm trong khoảng 0 - 100")]
        public decimal? DiscountRate { get; set; }

        [Display(Name = "Chính sách tích điểm")]
        public int? ConfigID { get; set; }

        [NotMapped]
        [Display(Name = "Chính sách tích điểm")]
        public string ConfigName { get; set; }
    }
}