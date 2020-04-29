using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    [Table("dbo.MemberCard")]
    public class MemberCard: BaseEntity
    {
        /// <summary>
        /// ID thẻ
        /// </summary>
        public long MemberCardID { get; set; }

        /// <summary>
        /// Tên thẻ
        /// </summary>
        [Display(Name = "Mã thẻ")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        [MaxLength(20, ErrorMessage = "Trường này tối đa 20 ký tự")]
        public string MemberCardNo { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        [Display(Name = "Số điện thoại")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Số điện thoại không hợp lệ")]
        [StringLength(25, MinimumLength = 10, ErrorMessage = "Trường này có độ dài từ 10 - 25 ký tự")]
        public string Tel { get; set; }

        /// <summary>
        /// ID hạng thẻ
        /// </summary>
        [Display(Name = "Hạng thẻ")]
        public long? MemberCardCategoryID { get; set; }

        /// <summary>
        /// Điểm
        /// </summary>
        [Display(Name = "Số điểm")]
        public decimal? Point { get; set; }

        /// <summary>
        /// Doanh thu
        /// </summary>
        [Display(Name = "Doanh thu")]
        public decimal? Revenue { get; set; }

        /// <summary>
        /// Ngày bắt đầu
        /// </summary>
        [Display(Name = "Ngày bắt đầu")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Ngày kết thúc
        /// </summary>
        [Display(Name = "Ngày kết thúc")]
        public DateTime? EndDate { get; set; }

        [NotMapped]
        [Display(Name = "Hạng thẻ")]
        public string MemberCardCategoryName { get; set; }
    }
}
