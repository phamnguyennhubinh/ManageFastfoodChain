using FastFoodChain16.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodChain16.Controllers
{
    public class NguoiDungController : Controller
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        // GET: NguoiDungController
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult DangNhap()
        {
            ViewData.Model = "Đăng nhập nah";
            var view = new ViewResult();
            view.ViewName = "/Views/NguoiDung/dangnhap.cshtml";
            view.ViewData = ViewData;
            return view;
        }
        public IActionResult ListUser()
        {
            IEnumerable<TaiKhoan> ds = da.TaiKhoans.Select(s => s);
            //List<TaiKhoan> ds = da.TaiKhoans.Select(s => s).ToList();
            return View(ds);  
        }
        //public IActionResult Oh()
        //public IActionResult DangKy()
        //{

        //}

        //GET: NguoiDungController/Details/5
        public ActionResult Details(int id)
        {
            TaiKhoan t = da.TaiKhoans.FirstOrDefault(s => s.MaTk == id);
            return View(t);
        }

        //Hiển thị giao diện thêm Tài khoản người dùng
        // GET: NguoiDungController/Create
        public ActionResult Create()
        {
            return View();
        }
        //Lấy tài khoản mới từ View
        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(TaiKhoan taikhoan, IFormCollection collection) //FormCollection collection: Toàn bộ số liệu trên form của view
        {
            try
            {
                //Tạo mới 1 đối tượng tài khoản t
                TaiKhoan t = new TaiKhoan();
                //Gán các thuộc tính cho tài khoản t
                t = taikhoan;
                //Thêm t vào bảng Tài khoản
                da.TaiKhoans.Add(t);
                //Lưu xuống database
                da.SaveChanges();
                //Gọi lại trang ds tài khoản
                return RedirectToAction("ListUser");
            }
            catch (Exception)
            {
                return View();
            }
        }
        // GET: NguoiDungController/Edit/5
        public ActionResult Edit(int id)
        {
            TaiKhoan t = da.TaiKhoans.FirstOrDefault(s => s.MaTk == id);
            return View(t);
        }

        // POST: NguoiDungController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaiKhoan taiKhoan, IFormCollection collection)
        {
            try
            {
                //Xác định tài khoản cầm sửa trong database
                TaiKhoan t = da.TaiKhoans.First(s=>s.MaTk== taiKhoan.MaTk);
                if (t != null && !string.IsNullOrEmpty(taiKhoan.TenDangNhap) && !string.IsNullOrEmpty(taiKhoan.MatKhau))
                {
                    //Gán các thuộc tính từ View cho t
                    t.TenDangNhap = taiKhoan.TenDangNhap;
                    t.MatKhau = taiKhoan.MatKhau;   
                    //Lưu xuống database
                    da.SaveChanges();
                    //Gọi lại trang dssp
                    return RedirectToAction("ListUser"); 
                }
                else
                {
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }
        // GET: NguoiDungController/Delete/5
        public ActionResult Delete(int id)
        {
            TaiKhoan t = da.TaiKhoans.FirstOrDefault(s=>s.MaTk == id);
            return View(t);
        }

        // POST: NguoiDungController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                //Lấy tài khoản muốn xóa
                TaiKhoan t = da.TaiKhoans.First(s => s.MaTk == id);
                //Xóa t khỏi tài khoản
                da.TaiKhoans.RemoveRange(t);  
                //Cập nhật lại db
                da.SaveChanges();
                return RedirectToAction("ListUser");
            }
            catch
            {
                return View();
            }
        }

       

    }
}
