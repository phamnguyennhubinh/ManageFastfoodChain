using FastFoodChain16.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FastFoodChain16.Controllers
{
    public class ThongKeController : Controller
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        private bool ShowResult = false;    
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ThongKeSLSP()
        {
            // Tạo danh sách năm
            List<int> years = new List<int>();
            for (int year = 2023; year <= DateTime.Now.Year; year++)
            {
                years.Add(year);
            }
            ViewBag.Years = new SelectList(years);

            // Tạo danh sách tháng
            List<int> months = new List<int>();
            for (int month = 1; month <= 12; month++)
            {
                months.Add(month);
            }
            ViewBag.Months = new SelectList(months);

            return View();
        }
        [HttpPost]
        public IActionResult ThongKeSLSP(int selectedMonth, int selectedYear)
        {
            // Gọi stored procedure với các tham số đã chọn
            var result = da.spMonthlySales(selectedYear, selectedMonth).ToList();

            // Đặt biến kiểm soát hiển thị kết quả thành true
            ViewBag.ShowResult = true;

            // Tạo danh sách năm và tháng
            List<int> years = new List<int>();
            for (int year = 2023; year <= DateTime.Now.Year; year++)
            {
                years.Add(year);
            }
            ViewBag.Years = new SelectList(years);

            List<int> months = new List<int>();
            for (int month = 1; month <= 12; month++)
            {
                months.Add(month);
            }
            ViewBag.Months = new SelectList(months);

            // Truyền kết quả và biến kiểm soát vào View
            return View("ThongKeSLSP", result);
        }
        [HttpGet]
        public IActionResult ThongKeDT()
        {
            List<int> months = new List<int>();
            for (int month = 1; month <= 12; month++)
            {
                months.Add(month);
            }
            ViewBag.Months = new SelectList(months);

            // Tạo danh sách năm
            List<int> years = new List<int>();
            for (int year = 2023; year <= DateTime.Now.Year; year++)
            {
                years.Add(year);
            }
            ViewBag.Years = new SelectList(years);

            return View();
        }
        [HttpPost]
        public IActionResult ThongKeDT(int thang, int nam)
        {
            // Gọi stored procedure với các tham số đã chọn
            var result = da.sp_ThongKeDoanhThu(thang, nam).ToList();

            // Đặt biến kiểm soát hiển thị kết quả thành true
            ViewBag.ShowResult = true;

            // Tạo danh sách năm và tháng
            List<int> years = new List<int>();
            for (int year = 2023; year <= DateTime.Now.Year; year++)
            {
                years.Add(year);
            }
            ViewBag.Years = new SelectList(years);

            List<int> months = new List<int>();
            for (int month = 1; month <= 12; month++)
            {
                months.Add(month);
            }
            ViewBag.Months = new SelectList(months);

            // Truyền kết quả và biến kiểm soát vào View
            return View("ThongKeDT", result);

        }

    }
}
