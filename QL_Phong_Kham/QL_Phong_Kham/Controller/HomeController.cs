using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace QL_Phong_Kham.Controllers
{
    public class HomeController : Controller
    {
        // Action mặc định khi truy cập trang chủ
        public IActionResult Index()
        {
            return View(); // Trả về View tương ứng (Index.cshtml)
        }

        // Hiển thị trang đăng nhập (GET request)
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return View(); // Trả về View đăng nhập (Login.cshtml)
        }

        // Xử lý yêu cầu đăng nhập (POST request)
        [HttpPost("/login")]
        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string password)
        {
            // Kiểm tra thông tin đăng nhập cứng (ví dụ: admin/password)
            if (username == "admin" && password == "password")
            {
                // Tạo danh sách các Claims (thông tin người dùng)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username), // Tên người dùng
                    new Claim(ClaimTypes.Role, "Administrator") // Vai trò người dùng
                };

                // Tạo ClaimsIdentity với các Claims và phương thức xác thực
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
                // Đăng nhập người dùng bằng cookie
                await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity));
                return Ok("Logged in successfully!"); // Trả về thông báo thành công
            }
            return Unauthorized(); // Trả về lỗi không được ủy quyền nếu thông tin sai
        }

        // Hiển thị trang đăng ký (GET request)
        [HttpGet("/register")]
        public IActionResult Register()
        {
            return View(); // Trả về View đăng ký (Register.cshtml)
        }

        // Xử lý yêu cầu đăng ký (POST request)
        [HttpPost("/register")]
        public IActionResult Register([FromForm] string username, [FromForm] string password)
        {
            // Kiểm tra xem tên người dùng và mật khẩu có rỗng không
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Trong ứng dụng thực tế, bạn sẽ lưu thông tin này vào cơ sở dữ liệu
                return Ok($"User {username} registered successfully!"); // Trả về thông báo đăng ký thành công
            }
            return BadRequest("Username and password are required."); // Trả về lỗi nếu thiếu thông tin
        }
    }
}
