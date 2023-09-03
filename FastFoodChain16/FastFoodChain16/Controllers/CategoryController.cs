using FastFoodChain16.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodChain16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        //Hien thi ds loaisp
        [HttpPost("Get All Category")]
        public IActionResult GetCategories()
        {
            var ds = da.LoaiSps.ToList();
            return Ok(ds);
        }
        [HttpGet("Get all Category get")]
        public IActionResult GetCategoryGet()
        {
            var ds = da.LoaiSps.OrderByDescending(s => s.MaLoai).ToList();
            return Ok(ds);
        }
        //Get Category by id
        [HttpGet("Get by CategoryID")]
        public IActionResult GetCategoryById(int id)
        {
            var ds = da.LoaiSps.FirstOrDefault(s => s.MaLoai == id);
            return Ok(ds);
        }
        //Them loai san pham
        [HttpPost("Add new Category")]
        public void AddCategories([FromBody] Category cate)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    LoaiSp c = new LoaiSp();
                    c.TenLoai = cate.TenLoai;
                    c.Discription = cate.Discription;
                    da.LoaiSps.Add(c);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
        private object SearchCategories(SearchCategoryReq searchCategoryReq)
        {
            var categories = da.LoaiSps.Where(x => x.TenLoai.Contains(searchCategoryReq.Keyword));
            var offset = (searchCategoryReq.Page - 1) * searchCategoryReq.Size;
            var total = categories.Count();
            int totalPage = (total % searchCategoryReq.Size) == 0 ? (int)(total / searchCategoryReq.Size) :
                (int)(1 + (total / searchCategoryReq.Size));
            var data = categories.OrderBy(x => x.MaLoai).Skip(offset).Take(searchCategoryReq.Size).ToList();
            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalPage,
                Page = searchCategoryReq.Page,
                Size = searchCategoryReq.Size
            };
            return res;
        }
        //Search Category
        [HttpPost("Search Category by ID")]
        public IActionResult SearchCategory([FromBody] SearchCategoryReq searchCategoryReq)
        {
            var category = SearchCategories(searchCategoryReq);
            return Ok(category);
        }
        //Edit Category
        [HttpPut("Edit a Category")]

        public void EditCategory([FromBody] Category cate)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    LoaiSp c = da.LoaiSps.First(s => s.MaLoai == cate.MaLoai);
                    c.TenLoai = cate.TenLoai;
                    c.Discription = cate.Discription;
                    da.LoaiSps.Update(c);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
        //Delete Category
        [HttpDelete("Delete a Category")]
        public void DeleteCategory(int id)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    LoaiSp c = da.LoaiSps.First(s => s.MaLoai == id);
                    da.LoaiSps.Remove(c);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
        //Thong ke so luong san pham theo loai san pham
        [HttpPost("Cal Products by Categories")]
        public IActionResult CalProductByCate()
        {
            var ds = da.SanPhams.GroupBy(s => s.MaLoai).Select(s => new { s.Key, sl = s.Count() }).ToList();
            return Ok(ds);
        }
    }
}
