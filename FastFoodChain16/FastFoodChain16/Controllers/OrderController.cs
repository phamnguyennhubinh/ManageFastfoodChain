using FastFoodChain16.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodChain16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        //Get All Order
        [HttpPost("Get All Orders")]
        public IActionResult GetAllOrder()
        {
            var ds = da.DonHangs.ToList();
            return Ok(ds);
        }
        //Get Order By ID
        [HttpGet("Get Order by ID")]
        public IActionResult GetOrderById(int id)
        {
            var ds = da.DonHangs.FirstOrDefault(s => s.MaDh == id);
            return Ok(ds);
        }
        //Add new Order
        [HttpPut("Add new Order")]
        public void AddOrder([FromBody] Order order)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    DonHang d = new DonHang();
                    d.MaDh = order.MaDH;
                    d.MaKh = order.MaKH;
                    d.MaNv = order.MaNV;
                    d.Omessage = order.OMessage;
                    d.NgayDat = order.NgayDat;
                    d.NgayGiao = order.NgayGiao;
                    d.DiaChiNhan = order.DiaChiNhan;
                    da.DonHangs.Add(d);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
        private object SearchOrders(SearchOrderReq searchOrderReq)
        {
            var order = da.DonHangs.Where(x => x.MaDh.ToString().Contains(searchOrderReq.Keyword));
            var offset = (searchOrderReq.Page - 1) * searchOrderReq.Size;
            var total = order.Count();
            int totalPage = (total % searchOrderReq.Size) == 0 ? (int)(total / searchOrderReq.Size) :
                (int)(1 + (total / searchOrderReq.Size));
            var data = order.OrderBy(x => x.MaDh).Skip(offset).Take(searchOrderReq.Size).ToList();
            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalPage,
                Page = searchOrderReq.Page,
                Size = searchOrderReq.Size
            };
            return res;
        }
        //Search Order
        [HttpPost("Search Order by ID")]
        public IActionResult SearchOrder([FromBody] SearchOrderReq searchOrderReq)
        {
            var order = SearchOrders(searchOrderReq);
            return Ok(order);
        }
        //Edit a Order
        [HttpPut("Edit a Order")]

        public void EditOrder([FromBody] Order order)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    DonHang d = da.DonHangs.First(s => s.MaDh == order.MaDH);
                    d.MaKh = order.MaKH;
                    d.MaNv = order.MaNV;
                    d.Omessage = order.OMessage;
                    d.NgayDat = order.NgayDat;
                    d.NgayGiao = order.NgayGiao;
                    d.DiaChiNhan = order.DiaChiNhan;
                    da.DonHangs.Update(d);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
        //Delete Order
        [HttpDelete("Delete a Order")]
        public void DeleteOrder(int id)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    DonHang d = da.DonHangs.First(s => s.MaDh == id);
                    da.DonHangs.Remove(d);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
        //Thong ke so luong don hang chua giao NgayGiao == NULL
        [HttpPost("Undelivered Orders")]
        public IActionResult UndeliveredOrders()
        {
            var order = da.DonHangs.Where(s => s.NgayGiao == null);
            var total = order.Count();
            var dsach = order.OrderBy(x=>x.MaDh);
            var ds = new { Data = dsach, sl = total };
            return Ok(ds);
        }
    }
}
