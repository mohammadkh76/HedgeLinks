using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalHospital.Models
{
    public class MenuPath
    {
        public int? Id { get; set; }
        [DisplayName("عنوان فارسی")]
        [Required(ErrorMessage = "وارد کردن عنوان صفحه الزامی می باشد.")]
        public string Title { get; set; }
        [DisplayName("عنوان عربی")]

        public string  Title_ar { get; set; }
        [DisplayName("عنوان انگلیسی")]

        public string Title_eng { get; set; }
        [DisplayName("شرح فارسی")]

        public string  Description{ get; set; }
        [DisplayName("شرح عربی")]

        public string  Description_ar { get; set; }
        [DisplayName("شرح انگلیسی")]

        public string Description_eng { get; set; }
        public string FilePath { get; set; }

        [DisplayName("نام صفحه")]
        [Required(ErrorMessage ="وارد کردن این قسمت الزامی است.")]
        public string  PageName{ get; set; }
    }
}