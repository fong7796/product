using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static MemberCardManagementV1.Constant.Enumeration;

namespace Model
{
    public class BaseEntity
    {
        /// <summary>
        /// Ngày tạo
        /// </summary>
        [Display(Name = "Ngày tạo")]
        public DateTime? CreateDate { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        [Display(Name = "Ngày cập nhật")]
        public DateTime? ModifyDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        [Display(Name = "Người tạo")]
        public Guid? CreateBy { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        [Display(Name = "Người cập nhật")]
        public Guid? ModifyBy { get; set; }

        /// <summary>
        /// Trạng thái: 1: Thêm, 2: Sửa
        /// </summary>
        [NotMapped]
        public EditMode? EditMode { get; set; }
    }
}
