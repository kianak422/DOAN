namespace QL_Phong_Kham.Models
{
    public class BenhNhan
    {
        public int Id { get; set; }
        public string HoTen { get; set; }
        public DateTime? NgaySinh { get; set; } // Đổi từ Tuoi sang NgaySinh
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string? GhiChu { get; set; } // Thêm thuộc tính GhiChu
    }
}

