using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace QL_Phong_Kham
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Thêm các dịch vụ vào container.
            builder.Services.AddControllersWithViews(); // Thêm dòng này để hỗ trợ MVC (Model-View-Controller)
            builder.Services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", config =>
            {
                config.Cookie.Name = "Grandmas.Cookie"; // Đặt tên cho cookie xác thực
                config.LoginPath = "/login"; // Đặt đường dẫn chuyển hướng khi người dùng chưa đăng nhập
            });
            builder.Services.AddAuthorization(); // Thêm dịch vụ ủy quyền (phân quyền)

            var app = builder.Build();

            // Cấu hình pipeline xử lý yêu cầu HTTP.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error"); // Sử dụng trang xử lý lỗi tùy chỉnh trong môi trường Production
                // Giá trị HSTS mặc định là 30 ngày. Bạn có thể thay đổi điều này cho các kịch bản Production, xem https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); // Buộc trình duyệt sử dụng HTTPS cho các yêu cầu tiếp theo
            }

            app.UseHttpsRedirection(); // Chuyển hướng các yêu cầu HTTP sang HTTPS
            app.UseStaticFiles(); // Cho phép phục vụ các tệp tĩnh (CSS, JS, hình ảnh)

            app.UseRouting(); // Kích hoạt tính năng định tuyến

            app.UseAuthentication(); // Kích hoạt middleware xác thực (đăng nhập)
            app.UseAuthorization(); // Kích hoạt middleware ủy quyền (phân quyền)

            // Định nghĩa tuyến đường mặc định cho các Controller
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run(); // Chạy ứng dụng
        }
    }
}