using Microsoft.AspNetCore.Mvc;
using QL_Phong_Kham.Models;

namespace QL_Phong_Kham.Controllers
{
    public class LichHenController : Controller
    {
        private static List<LichHen> danhSachLichHen = new();

        // Trang cho nhân viên xem và xử lý lịch hẹn
        public IActionResult Quan_Ly_Lich_Hen_NV()
        {
            ViewBag.DanhSachLichHen = danhSachLichHen;
            return View();
        }

        [HttpPost]
        public IActionResult XuLyLichHen(int id, bool chapNhan, string? thoiGianKham, string? ghiChuNhanVien)
        {
            var lich = danhSachLichHen.FirstOrDefault(x => x.Id == id);
            if (lich != null)
            {
                lich.ChapNhan = chapNhan;
                lich.GhiChuNhanVien = ghiChuNhanVien;
                if (chapNhan && DateTime.TryParse(thoiGianKham, out var tg))
                    lich.ThoiGianKham = tg;
            }
            return RedirectToAction("Quan_Ly_Lich_Hen_NV");
        }

        // Trang cho khách hàng đặt lịch
        public IActionResult Quan_Ly_Lich_Hen_KH()
        {
            ViewBag.BenhViens = new List<string>
            {
                "BV ĐKQT Vinmec Central Park (Hồ Chí Minh)",
                "BV ĐKQT Vinmec Times City (Hà Nội)",
                "BV ĐKQT Vinmec Đà Nẵng",
                "Phòng khám ĐKQT Vinmec Sài Gòn"
            };
            ViewBag.ChuyenKhoas = new List<string>
    {
        "Nội tổng quát",
        "Ngoại tổng quát",
        "Nhi khoa",
        "Sản phụ khoa"
    };
            ViewBag.BacSis = new List<string>
    {
        "BS. Nguyễn Văn A",
        "BS. Trần Thị B",
        "BS. Lê Văn C"
    };
            return View();
        }

        [HttpPost]
        public IActionResult DatLich(LichHen lich)
        {
            lich.Id = danhSachLichHen.Count + 1;
            lich.ChapNhan = false;
            danhSachLichHen.Add(lich);
            return RedirectToAction("Quan_Ly_Lich_Hen_KH");
        }
    }
}