using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MemberCardManagementV1.Models
{
    [Table("dbo.Config")]
    public class Config: BaseEntity
    {
        public int ConfigID { get; set; }

        [Display(Name = "Tên chính sách tích điểm")]
        [Required(ErrorMessage = "Trường này không được để trống")]
        public string ConfigName { get; set; }

        [Display(Name = "Giá trị tích điểm (VND)")]
        [Range(1, long.MaxValue, ErrorMessage = "Giá trị nhỏ nhất của trường này là 1")]
        public decimal? ConfigValue { get; set; }

        [Display(Name = "Là chính sách mặc định")]
        public bool? IsDefault { get; set; }
    }
}