using FastFoodChain16.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FastFoodChain16.Controllers
{
    public class KhachHangController : Controller
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        public IActionResult Index()
        {
            return View("Login","KhachHang");
        }
        public IActionResult Create()
        {
            return View();
        }
        //Lấy tài khoản mới từ View
        // POST: Product/Create
        [HttpPost]
        public IActionResult Create(KhachHang khachHang, IFormCollection collection) //FormCollection collection: Toàn bộ số liệu trên form của view
        {
            try
            {
                
                var sdt = collection["Sdt"];

                // Kiểm tra xem số điện thoại đã tồn tại trong cơ sở dữ liệu hay chưa
                var existingKhachHang = da.KhachHangs.FirstOrDefault(kh => kh.Sdt == sdt);

                if (existingKhachHang != null)
                {
                    // Số điện thoại đã tồn tại trong cơ sở dữ liệu
                    ViewData["Loi"] = "Số điện thoại đã tồn tại.";
                    return View();
                }
                KhachHang kh = new KhachHang();
                //Gán các thuộc tính cho khách hàng kh
                kh = khachHang;

                // Thêm khách hàng vào bảng Khách hàng
                da.KhachHangs.Add(kh);
                HttpContext.Session.SetString("SDT", khachHang.Sdt);

                // Lưu xuống database
                da.SaveChanges();

                // Gọi lại trang 
                return RedirectToAction("CreateTK", "KhachHang");
            }
            catch (Exception)
            {
                return View();
            }
        }
        public IActionResult CreateTK()
        {
            return View();
        }
        //Lấy tài khoản mới từ View
        // POST: Product/Create
        [HttpPost]
        public IActionResult CreateTK(TaiKhoan taiKhoan, IFormCollection collection) //FormCollection collection: Toàn bộ số liệu trên form của view
        {
            try
            {
                var tendn = collection["TenDangNhap"];
                var pass = collection["MatKhau"];
                //Tạo mới 1 đối tượng tài khoản
                //TaiKhoan tk = new TaiKhoan();
                //Gán các thuộc tính cho tài khoản
                var same = HttpContext.Session.GetString("SDT");
                if (tendn==same)
                {
                    if(IsStrongPassword(pass))
                    {
                        TaiKhoan tk = new TaiKhoan();
                        tk = taiKhoan;
                        //Thêm kh vào bảng tài khoản
                        da.TaiKhoans.Add(tk);
                        //Lưu xuống database
                        da.SaveChanges();
                        //Gọi lại trang 
                        return RedirectToAction("Login", "KhachHang");
                    }    
                    else
                    {
                        ViewData["Loi1"] = "Mật khẩu chưa đủ mạnh. Mật khẩu chứa ít nhất 8 ký tự, trong đó có ít nhất 1 chữ cái viết hoa, 1 chữ cái viết thường và 1 số";
                        return View();
                    }    
                } 
                else
                {
                    ViewData["Loi2"] = "Tên đăng nhập phải là số điện thoại.";
                    return View();
                }    
               
            }
            catch (Exception)
            {
                return View();
            }
        }
        private bool IsPhoneNumber(string input)
        {
            // Định dạng số điện thoại (ví dụ: 10 chữ số, bắt đầu bằng số 0)
            return input.Length == 10 && input.StartsWith("0") && input.All(char.IsDigit);
        }

        private bool IsStrongPassword(string input)
        {
            // Điều kiện kiểm tra mật khẩu mạnh
            return input.Length >= 8 && input.Any(char.IsUpper) && input.Any(char.IsLower) && input.Any(char.IsDigit);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(IFormCollection collection, TaiKhoan taiKhoan)
        {
            //Gán các giá trị người dùng nhập liệu cho các biến
            var tendn = collection["TenDangNhap"].ToString();
            var matkhau = collection["MatKhau"].ToString();
            if (string.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Vui lòng nhập tên đăng nhập";
            }
            else if (string.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Vui lòng nhập mật khẩu";
            }
            else
            {
                TaiKhoan tk = da.TaiKhoans.SingleOrDefault(n => n.TenDangNhap == tendn && n.MatKhau == matkhau);
                if (tk != null)
                {
                    HttpContext.Session.SetInt32("MaTK", tk.MaTk);
                    ViewBag.Thongbao = "Chúc mừng bạn đăng nhập thành công!";
                    //return RedirectToAction("ListProduct", "KhachHang"); 
                    if (matkhau.Contains("Admin"))
                    {
                        return RedirectToAction("ListProduct", "SanPham");
                    }
                    else
                    {
                        return RedirectToAction("ListProduct", "KhachHang");
                    }    

                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng.";

            }
            return View();
        }

        public IActionResult XemDH()
        {
            var maTK = HttpContext.Session.GetInt32("MaTK");
            if (maTK.HasValue)
            {
                // Sử dụng maTK để truy vấn hoặc truyền vào Stored Procedure
                var results = da.spDonHang(maTK.Value).ToList();
                // Xử lý kết quả
                return View(results);
            }
            else
            {
                // Xử lý khi maTK không tồn tại trong Session
                return RedirectToAction("Login"); // Hoặc điều hướng đến trang đăng nhập khác
            }
        }
        public ActionResult ListProduct(string SearchString)
        {
            ////List<SanPham> listProduct = da.SanPhams.ToList() ;
            //var listProduct = da.SanPhams.Where(s => s.TenSp.Contains(SearchString)).ToList();
            //return View(listProduct);
            var listProduct = da.SanPhams.ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                listProduct = listProduct.Where(s => s.TenSp.IndexOf(SearchString, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            return View(listProduct);
        }
        public ActionResult Details(int id)
        {
            SanPham p = da.SanPhams.Where(q => q.MaSp == id).FirstOrDefault();
            return View(p);

        }
        public IActionResult Search(string keyword)
        {
            return View();
        }
 
    }
}

