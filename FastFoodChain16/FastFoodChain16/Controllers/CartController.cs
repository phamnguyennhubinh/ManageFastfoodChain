using System.Transactions;
using FastFoodChain16.Helper;
using FastFoodChain16.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace FastFoodChain16.Controllers
{
    public class CartController : Controller
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        public List<Cart> GetListCarts
        {
            get
            {
                var data = HttpContext.Session.Get<List<Cart>>("Carts");
                if (data == null)
                {
                    data = new List<Cart>();
                }
                return data;
            }
        }
        public IActionResult ListCart()
        {
            var myCart = GetListCarts;
            ViewBag.CountProduct = myCart.Sum(s => s.SoLuong);
            ViewBag.Total = myCart.Sum(s => s.Total);
            return View(GetListCarts);
        }
        public IActionResult AddToCart(int id)
        {
            var myCart = GetListCarts;
            var item = myCart.SingleOrDefault(p => p.MaSp == id);
            if (item == null)
            {
                var sanpham = da.SanPhams.SingleOrDefault(p => p.MaSp == id);
                item = new Cart
                {
                    MaSp = id,
                    TenSp = sanpham.TenSp,
                    DonGia = sanpham.DonGia,
                    SoLuong = 1,

                };
                myCart.Add(item);
            }
            else
            {
                item.SoLuong++;
            }
            HttpContext.Session.Set("Carts", myCart);
            return RedirectToAction("ListCart");

        }
        public IActionResult RemoveFromCart(int id)
        {
            var myCart = GetListCarts;
            var item = myCart.SingleOrDefault(p => p.MaSp == id);
            if (item != null)
            {
                myCart.Remove(item);
            }
            HttpContext.Session.Set("Carts", myCart);
            return RedirectToAction("ListCart");
        }
        public IActionResult UpdateCart(int id, int quantity)
        {
            var myCart = GetListCarts;
            var item = myCart.SingleOrDefault(p => p.MaSp == id);
            if (item != null)
            {
                item.SoLuong = quantity;
            }
            HttpContext.Session.Set("Carts", myCart);
            return RedirectToAction("ListCart");
        }
        public ActionResult OrderProduct()
        {
            using (TransactionScope tranScope = new TransactionScope())
                try
                {
                    //1.Tạo một đơn hàng mới cho bảng Order
                    //1.1 Tạo orders order
                    DonHang p = new DonHang();
                    //1.2 Gán thuộc tính cho order
                    p.NgayDat = DateTime.Now;
                    //1.3 Add vào bảng Order
                    da.DonHangs.Add(p);
                    //1.4 Cập nhật database
                    da.SaveChanges();
                    //2. Thêm các SP của đơn hàng đó và OrderDetails
                    //2.1 Duyệt từng sản phẩm trong giỏ hàng
                    foreach (Cart item in GetListCarts)
                    {
                        //2.2 Tạo mới một đối tượng OrderDetails
                        ChiTietDonHang orderDetail = new ChiTietDonHang();
                        //2.3 Gán thuộc tính cho OrderDetails
                        orderDetail.MaDh = p.MaDh;
                        orderDetail.MaSp = item.MaSp;
                        orderDetail.DonGia = decimal.Parse(item.DonGia.ToString());
                        orderDetail.SoLuong = short.Parse(item.SoLuong.ToString());
                        orderDetail.Discount = 0;
                        //2.4  Add vào bảng OrderDetails
                        da.ChiTietDonHangs.Add(orderDetail);

                    }
                    //2.5 Cập nhật database
                    da.SaveChanges();
                    tranScope.Complete(); //Hoan tat trans
                }
                catch (SqlException exx)
                {
                    tranScope.Dispose();
                    return RedirectToAction("ListOrders");//sưar
                }
            return RedirectToAction("ListOrders","KhachHang");//Sửa
        }
        public ActionResult ListOrders()
        {
            var sp = da.DonHangs.OrderByDescending(s => s.NgayDat).ToList();
            return View(sp);
        }

    }
}

