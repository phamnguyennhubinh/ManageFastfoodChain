using FastFoodChain16.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace FastFoodChain16.Controllers
{
    public class KhachHangController : Controller
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        public IActionResult Index()
        {
            return View();
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
                //Tạo mới 1 đối tượng khách hàng kh
                KhachHang kh = new KhachHang();
                //Gán các thuộc tính cho khách hàng kh
                kh = khachHang;
                //Thêm kh vào bảng Khách hàng
                da.KhachHangs.Add(kh);
                //Lưu xuống database
                da.SaveChanges();
                //Gọi lại trang 
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
                //Tạo mới 1 đối tượng khách hàng kh
                TaiKhoan tk = new TaiKhoan();
                //Gán các thuộc tính cho khách hàng kh
                tk = taiKhoan;
                //Thêm kh vào bảng Khách hàng
                da.TaiKhoans.Add(tk);
                //Lưu xuống database
                da.SaveChanges();
                //Gọi lại trang 
                return RedirectToAction("Login", "KhachHang");
            }
            catch (Exception)
            {
                return View();
            }
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
                    return RedirectToAction("ListProduct", "KhachHang"); 

                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng.";

            }
            return View();
        }
        public IActionResult XemDH()
        {
            var ma = HttpContext.Session.GetInt32("MaTK");
            if (ma.HasValue)
            {
                int maTK = ma.Value; // Chuyển đổi từ int? sang int

                // Sử dụng maTK để truy vấn hoặc truyền vào Stored Procedure
               
                var results = da.spDonHang(maTK).ToList();
                // Xử lý kết quả
                return View(results);
            }
            else
            {
                // Xử lý khi maTK không tồn tại trong Session
                return RedirectToAction("Login"); // Hoặc điều hướng đến trang đăng nhập khác
            }
        }
        //
        public ActionResult ListProduct(string SearchString)
        {
            ////List<SanPham> listProduct = da.SanPhams.ToList() ;
            //var listProduct = da.SanPhams.Where(s => s.TenSp.Contains(SearchString)).ToList();
            //return View(listProduct);
            var listProduct = da.SanPhams.ToList();

            if (!string.IsNullOrEmpty(SearchString))
            {
                listProduct = listProduct.Where(s => s.TenSp.Contains(SearchString)).ToList();
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

