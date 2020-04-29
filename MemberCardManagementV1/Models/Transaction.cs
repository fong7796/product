using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MemberCardManagementV1.Models
{
    [Table("dbo.Transaction")]
    public class Transaction : BaseEntity
    {
        public long TransactionID { get; set; }

        [Display(Name = "Thẻ tích điểm")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public long MemberCardID { get; set; }

        [NotMapped]
        [Display(Name = "Thẻ tích điểm")]
        public string MemberCardNo { get; set; }

        [Display(Name = "Số điểm điều chỉnh")]
        //[Required(ErrorMessage = "Trường này không được để trống")]
        [Range(-9999999999, 9999999999, ErrorMessage = "Trường này nằm trong khoảng -9.999.999.999 - 9.999.999.999")]
        public decimal? Point { get; set; }

        [Display(Name = "Số doanh thu điều chỉnh (VND)")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        [Range(-9999999999, 9999999999, ErrorMessage = "Trường này nằm trong khoảng -9.999.999.999 - 9.999.999.999")]
        public decimal Revenue { get; set; }
    }
}