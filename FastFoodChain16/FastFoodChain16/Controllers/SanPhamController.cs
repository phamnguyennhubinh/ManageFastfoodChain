using FastFoodChain16.Models;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodChain16.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        // GET: SanPhamController
        public ActionResult ListProduct()
        {
            List<SanPham> listProduct = da.SanPhams.ToList();
            return View(listProduct);
        }

        // GET: SanPhamController/Details/5
        public ActionResult Details(int id)
        {
            SanPham p = da.SanPhams.Where(q => q.MaSp == id).FirstOrDefault();
            return View(p);

        }

        // GET: SanPhamController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SanPhamController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection, SanPham sanpham)
        {
            try
            {
                //Lay SP tra ve
                SanPham p = sanpham;
                //Them vao bang Product
                da.SanPhams.Add(p);
                //Cap nhat xuong database
                da.SaveChanges();
                //Hien thi lai DS
                return RedirectToAction("ListProduct");
            }
            catch
            {
                return View();
            }
        }

        // GET: SanPhamController/Edit/5
        public ActionResult Edit(int id)
        {
            SanPham p = da.SanPhams.FirstOrDefault(s => s.MaSp == id);
            return View(p);
        }

        // POST: SanPhamController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection, SanPham sanpham)
        {
            try
            {

                //Xu ly Sua
                //Lay SP co san trong db
                SanPham p = da.SanPhams.First(s => s.MaSp == sanpham.MaSp);
                //Thuc hien sua theo thong tin SP truyen vao tu View
                p.TenSp = sanpham.TenSp;
                p.MaNcc = sanpham.MaNcc;
                p.MaLoai = sanpham.MaLoai;
                p.MoTa = sanpham.MoTa;
                p.DonGia = sanpham.DonGia;
                //Cap nhat xuong database
                //p.MaLoai = int.Parse(collection["LSP"]);
                da.SaveChanges();
                //Hien thi lai DS
                return RedirectToAction("ListProduct");
            }
            catch
            {
                return View();
            }
        }

        // GET: SanPhamController/Delete/5
        public ActionResult Delete(int id)
        {
            SanPham p = da.SanPhams.FirstOrDefault(s => s.MaSp == id);
            return View(p);

        }

        // POST: SanPhamController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                SanPham p = da.SanPhams.First(s => s.MaSp == id);
                da.SanPhams.Remove(p);
                da.SaveChanges();
                return RedirectToAction("ListProduct");
            }
            catch
            {
                return View();
            }
        }
    }
}
