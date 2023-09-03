using FastFoodChain16.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodChain16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        //Hien thi ds nha cung cap
        [HttpPost("Get All Supplier")]
        public IActionResult GetSuppliers()
        {
            var ds = da.NhaCungCaps.ToList();
            return Ok(ds);
        }
        [HttpGet("Get all Supplier get")]
        public IActionResult GetSupplierGet()
        {
            var ds = da.NhaCungCaps.OrderByDescending(s => s.MaNcc).ToList();
            return Ok(ds);
        }
        //Get Supplier by id
        [HttpGet("Get by SupplierID")]
        public IActionResult GetSupplierId(int id)
        {
            var ds = da.NhaCungCaps.FirstOrDefault(s => s.MaNcc == id);
            return Ok(ds);
        }
        //Them nha cung cap
        [HttpPost("Add new Supplier")]
        public void AddSupplier([FromBody] Supplier supplier)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    NhaCungCap c = new NhaCungCap();
                    c.MaNcc = supplier.SupplierID;
                    c.TenNcc = supplier.SupplierName;
                    c.Sdt = supplier.SupplierPhone;
                    c.DiaChi = supplier.SupplierAddress;
                    da.NhaCungCaps.Add(c);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
        private object SearchSuppliers(SearchSupplierReq searchSupplierReq)
        {
            var suppliers = da.NhaCungCaps.Where(x => x.MaNcc.ToString().Contains(searchSupplierReq.Keyword));
            var offset = (searchSupplierReq.Page - 1) * searchSupplierReq.Size;
            var total = suppliers.Count();
            int totalPage = (total % searchSupplierReq.Size) == 0 ? (int)(total / searchSupplierReq.Size) :
                (int)(1 + (total / searchSupplierReq.Size));
            var data = suppliers.OrderBy(x => x.MaNcc).Skip(offset).Take(searchSupplierReq.Size).ToList();
            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalPage,
                Page = searchSupplierReq.Page,
                Size = searchSupplierReq.Size
            };
            return res;
        }
        //Search Category
        [HttpPost("Search Supplier by ID")]
        public IActionResult SearchSupplier([FromBody] SearchSupplierReq searchSupplierReq)
        {
            var supplier = SearchSuppliers(searchSupplierReq);
            return Ok(supplier);
        }
        //Edit Supplier
        [HttpPut("Edit Supplier")]

        public void EditSupplier([FromBody] Supplier supplier)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    NhaCungCap c = da.NhaCungCaps.First(s => s.MaNcc == supplier.SupplierID);
                    c.MaNcc = supplier.SupplierID;
                    c.TenNcc = supplier.SupplierName;
                    c.Sdt = supplier.SupplierPhone;
                    c.DiaChi = supplier.SupplierAddress;
                    da.NhaCungCaps.Update(c);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
        //Delete Supplier
        [HttpDelete("Delete a Supplier")]
        public void DeleteSupplier(int id)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    NhaCungCap c = da.NhaCungCaps.First(s => s.MaNcc == id);
                    da.NhaCungCaps.Remove(c);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
    }
}
