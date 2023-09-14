using System.Transactions;
using FastFoodChain16.DTO;
using FastFoodChain16.Helper;
using FastFoodChain16.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

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
                if (item.SoLuong > 1)
                {
                    item.SoLuong -= 1;
                }
                else
                {
                    myCart.Remove(item);
                }
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

        public IActionResult OrderProduct(IFormCollection collection, DonHang donHang)
        {
            //using (TransactionScope tranScope = new TransactionScope())
            //    try
            //    {
                    var diachinhan = collection["DiaChiNhan"].ToString();
                    DonHang p = new DonHang();
                    if (string.IsNullOrEmpty(diachinhan))
                    {
                        ViewData["Loi2"] = "Vui lòng nhập địa chỉ nhận";
                    } 
                    else
                    {

                //1.Tạo một đơn hàng mới cho bảng Order
                //1.1 Tạo orders order
                //DonHang p = new DonHang();
                //1.2 Gán thuộc tính cho order'
                var ma = HttpContext.Session.GetInt32("MaTK");
                var entity = da.MyEntities.FromSqlRaw("SELECT * FROM dbo.fGetMaKH({0})", ma).FirstOrDefault();
                int? maKHValue = entity.MaKH; // Giả sử Id là thuộc tính kiểu int trong TEntity


                // Xử lý kết quả
                if (maKHValue != null)
                         {
                           
                            p.MaKh = maKHValue;
                            p.Omessage = donHang.Omessage;
                            p.DiaChiNhan = donHang.DiaChiNhan;
                            p.NgayDat = DateTime.Now;
                            p.NgayGiao = null;
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
                                orderDetail.MaTk = ma;
                                orderDetail.Discount = 0;
                                //2.4  Add vào bảng OrderDetails
                                da.ChiTietDonHangs.Add(orderDetail);

                            }
                            //2.5 Cập nhật database
                            da.SaveChanges();
                    //tranScope.Complete(); //Hoan tat trans
                    return RedirectToAction("XemDH", "KhachHang", new {maTK = ma});
                        }
                            else
                            {
                                // Xử lý khi không tìm thấy MaKH hoặc MaKH rỗng
                                return RedirectToAction("Login"); // Hoặc điều hướng đến trang đăng nhập khác
                            }
            }
            //}
            //catch (Exception)
            //{
            //    tranScope.Dispose();
            //    return RedirectToAction("ListCart","Cart");
            //}
            return View();
        }
        public IActionResult ListOrders()
        {
            var sp = da.DonHangs.OrderByDescending(s => s.NgayDat).ToList();
            return View(sp);
        }

    }
}

