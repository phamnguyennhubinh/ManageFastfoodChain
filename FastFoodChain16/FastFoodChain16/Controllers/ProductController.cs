using FastFoodChain16.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FastFoodChain16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        //Hien thi dssp
        [HttpPost("Get All Products")]
        public IActionResult GetProducts()
        {
            var ds = da.SanPhams.ToList();
            return Ok(ds);
        }
        //Hien thi dssp theo id
        [HttpGet("Get by id ")]
        public IActionResult GetProductsById(int id)
        {
            var ds = da.SanPhams.FirstOrDefault(s => s.MaSp == id);
            return Ok(ds);
        }
        //Tim kiem san pham 
        [HttpPost(" Search Product by Id")]
        public IActionResult SearchProduct([FromBody] SearchProductReq searchProductReq)
        {
            var sanpham = SearchProducts(searchProductReq);
            return Ok(sanpham);
        }
        [HttpPost("Get All Products Get")]
        public IActionResult GetProductsGet()
        {
            var ds = da.SanPhams.OrderByDescending(s => s.MaSp).ToList();
            return Ok(ds);
        }
        //Them Sp
        [HttpPost(" Add new Product")]
        public void AddProduct([FromBody] Product product)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    SanPham p = new SanPham();
                    p.TenSp = product.TenSp;
                    p.DonGia = product.DonGia;
                    p.MoTa = product.MoTa;
                    p.MaLoai = product.MaLoai;
                    p.MaNcc = product.MaNcc;

                    da.SanPhams.Add(p);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
        //Sua sp
        [HttpPut(" Edit a Product")]
        public void EditProduct([FromBody] Product product)
        {
            SanPham p = da.SanPhams.First(s => s.MaSp == product.MaSp);
            p.TenSp = product.TenSp;
            p.DonGia = product.DonGia;
            p.MoTa = product.MoTa;
            p.MaLoai = product.MaLoai;
            p.MaNcc = product.MaNcc;

            da.SanPhams.Update(p);
            da.SaveChanges();
        }
        //Xoa SP
        [HttpDelete(" Delete a Product")]
        public void DeleteProduct(int id)
        {
            try
            {
                SanPham p = da.SanPhams.First(s => s.MaSp == id);
                da.SanPhams.Remove(p);
                da.SaveChanges();
            }
            catch (Exception)
            {


            }

        }
        private object SearchProducts(SearchProductReq searchProductReq)
        {
            // Lay DS SP theo tu khoa
            var sanpham = da.SanPhams.Where(x => x.TenSp.Contains(searchProductReq.Keyword));
            // Xu ly phan trang 
            var offset = (searchProductReq.Page - 1) * searchProductReq.Size;
            var total = sanpham.Count();
            int totalPage = (total % searchProductReq.Size) == 0 ? (int)(total / searchProductReq.Size) : (int)(1 + (total / searchProductReq.Size));
            var data = sanpham.OrderBy(x => x.MaSp).Skip(offset).Take(searchProductReq.Size).ToList();
            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalPage,
                Page = searchProductReq,
                Size = searchProductReq.Size,
            };
            return res;
        }
    }
}
