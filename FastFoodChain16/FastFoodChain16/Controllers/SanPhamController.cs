using FastFoodChain16.Models;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodChain16.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult ListProduct()
        {

            IEnumerable<SanPham> ds = da.SanPhams.Select(s=>s);
            return View(ds);
        }    
    }
}
