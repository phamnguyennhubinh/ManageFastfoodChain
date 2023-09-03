using FastFoodChain16.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FastFoodChain16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        //Hien thi danh sach KH
        [HttpPost("Get All Customer")]
        public IActionResult GetCustomer()
        {
            var ds = da.KhachHangs.ToList();
            return Ok(ds);
        }
        //Hien thi dsKH theo id
        [HttpGet("Get by id ")]
        public IActionResult GetCustomerById(int id)
        {
            var ds = da.KhachHangs.FirstOrDefault(s => s.MaKh == id);
            return Ok(ds);
        }
        //Tim kiem KH
        [HttpPost(" Search Customer by Id")]
        public IActionResult SearchCustomer([FromBody] SearchCustomerReq searchCustomerReq)
        {
            var khachhang = SearchCustomers(searchCustomerReq);
            return Ok(khachhang);
        }
        [HttpPost("List Customer no order")]
        public IActionResult ListCustomerNoOrder()
        {
            using (var tran = new QuanLyBanFastFood16Context())
            {
                var ds = da.KhachHangs.FromSqlRaw("exec spDSKHchuaDH").ToList();
                return Ok(ds);

            }

        }
        //Them KH
        [HttpPost(" Add new Customer")]
        public void AddCustomer([FromBody] Customer customer)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    KhachHang p = new KhachHang();
                    p.TenKh = customer.TenKh;
                    p.HoKh = customer.HoKh;
                    p.Sdt = customer.Sdt;
                    p.DiaChi = customer.DiaChi;
                    p.Email = customer.Email;

                    da.KhachHangs.Add(p);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
        //Sua KH
        [HttpPut(" Edit a Customer")]
        public void EditCustomer([FromBody] Customer customer)
        {
            KhachHang p = da.KhachHangs.First(s => s.MaKh == customer.MaKh);
            p.TenKh = customer.TenKh;
            p.HoKh = customer.HoKh;
            p.Sdt = customer.Sdt;
            p.DiaChi = customer.DiaChi;
            p.Email = customer.Email;

            da.KhachHangs.Update(p);
            da.SaveChanges();
        }
        //Xoa KH
        [HttpDelete(" Delete a Customer")]
        public void DeleteProduct(int id)
        {
            try
            {
                KhachHang p = da.KhachHangs.First(s => s.MaKh == id);
                da.KhachHangs.Remove(p);
                da.SaveChanges();
            }
            catch (Exception)
            {

            }
        }
        private object SearchCustomers(SearchCustomerReq searchCustomerReq)
        {
            var khachhang = da.KhachHangs.Where(x => x.TenKh.Contains(searchCustomerReq.Keyword));
            var offset = (searchCustomerReq.Page - 1) * searchCustomerReq.Size;
            var total = khachhang.Count();
            int totalPage = (total % searchCustomerReq.Size) == 0 ? (int)(total / searchCustomerReq.Size) :
                (int)(1 + (total / searchCustomerReq.Size));
            var data = khachhang.OrderBy(x => x.MaKh).Skip(offset).Take(searchCustomerReq.Size).ToList();
            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPages = totalPage,
                Page = searchCustomerReq.Page,
                Size = searchCustomerReq.Size
            };
            return res;
        }

    }
}
