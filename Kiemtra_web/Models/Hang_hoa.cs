using System.ComponentModel.DataAnnotations;

namespace Kiemtra_web.Models
{
    public class Hang_hoa
    {
        [Key]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "Mã hàng hóa phải có đúng 9 ký tự.")]
        [Required(ErrorMessage = "Mã hàng hóa không được để trống.")]
        public string MaHangHoa { get; set; }

        [Required(ErrorMessage = "Tên hàng hóa không được để trống.")]
        public string TenHangHoa { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải là số nguyên dương hoặc bằng 0.")]
        public int SoLuong { get; set; }

        public string? ghi_chu { get; set; }
    }
}
