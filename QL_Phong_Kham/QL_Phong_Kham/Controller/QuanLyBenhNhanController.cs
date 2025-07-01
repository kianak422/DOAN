using Microsoft.AspNetCore.Mvc;
using QL_Phong_Kham.Models;
namespace QL_Phong_Kham.Controllers
{

    public class QuanLyBenhNhanController : Controller
    {
        private static List<BenhNhan> danhSach = new List<BenhNhan>();
        private static int idCounter = 1;

        public IActionResult Quan_ly_benh_nhan()
        {
            ViewBag.DanhSachBenhNhan = danhSach;
            return View();
        }

        [HttpPost]
        public IActionResult AddOrUpdate(BenhNhan bn)
        {
            if (bn.Id == 0)
            {
                bn.Id = idCounter++;
                danhSach.Add(bn);
            }
            else
            {
                var existing = danhSach.FirstOrDefault(b => b.Id == bn.Id);
                if (existing != null)
                {
                    existing.HoTen = bn.HoTen;
                    existing.NgaySinh = bn.NgaySinh;
                    existing.GioiTinh = bn.GioiTinh;
                    existing.DiaChi = bn.DiaChi;
                    existing.GhiChu = bn.GhiChu; // Cập nhật GhiChu
                }
            }

            return RedirectToAction("Quan_ly_benh_nhan");
        }

        public IActionResult Edit(int id)
        {
            var bn = danhSach.FirstOrDefault(b => b.Id == id);
            ViewBag.BenhNhan = bn;
            ViewBag.DanhSachBenhNhan = danhSach;
            return View("Quan_ly_benh_nhan");
        }

        public IActionResult Delete(int id)
        {
            var bn = danhSach.FirstOrDefault(b => b.Id == id);
            if (bn != null) danhSach.Remove(bn);
            return RedirectToAction("Quan_ly_benh_nhan");
        }
    }
}
