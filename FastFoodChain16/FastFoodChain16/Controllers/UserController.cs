using FastFoodChain16.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFoodChain16.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        QuanLyBanFastFood16Context da = new QuanLyBanFastFood16Context();
        //Hien thi tai khhoan nguoi dung
        [HttpPost("Get All User")]
        public IActionResult GetUser()
        {
            var ds = da.TaiKhoans.ToList();
            return Ok(ds);
        }
        [HttpGet("Get all User get")]
        public IActionResult GetUserGet()
        {
            var ds = da.TaiKhoans.OrderByDescending(s => s.MaTk).ToList();
            return Ok(ds);
        }
        //Get User by id
        [HttpGet("Get by UserID")]
        public IActionResult GetUserById(int id)
        {
            var ds = da.TaiKhoans.FirstOrDefault(s => s.MaTk== id);
            return Ok(ds);
        }
        //Them tai khoan nguoi dung
        [HttpPost("Add new User")]
        public void AddUser([FromBody] User user)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    TaiKhoan c = new TaiKhoan();
                    c.MaTk = user.UserID;
                    c.TenDangNhap = user.UserName;
                    c.MatKhau = user.Password;  
                    da.TaiKhoans.Add(c);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
        private object SearchUsers(SearchUserReq searchUserReq)
        {
            var users = da.TaiKhoans.Where(x => x.MaTk.ToString().Contains(searchUserReq.Keyword));
            var offset = (searchUserReq.Page - 1) * searchUserReq.Size;
            var total = users.Count();
            int totalPage = (total % searchUserReq.Size) == 0 ? (int)(total / searchUserReq.Size) :
                (int)(1 + (total / searchUserReq.Size));
            var data = users.OrderBy(x => x.MaTk).Skip(offset).Take(searchUserReq.Size).ToList();
            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPage = totalPage,
                Page = searchUserReq.Page,
                Size = searchUserReq.Size
            };
            return res;
        }
        //Search User
        [HttpPost("Search User by UserID")]
        public IActionResult SearchUser([FromBody] SearchUserReq searchUserReq)
        {
            var user = SearchUsers(searchUserReq);
            return Ok(user);
        }
        //Edit Category
        [HttpPut("Edit a User")]

        public void EditUser([FromBody] User user)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    TaiKhoan c = da.TaiKhoans.First(s => s.MaTk == user.UserID);
                    c.TenDangNhap = user.UserName;
                    c.MatKhau = user.Password;
                    da.TaiKhoans.Update(c);
                    da.SaveChanges();
                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                }
            }
        }
        //Delete User
        [HttpDelete("Delete a User")]
        public void DeleteUser(int id)
        {
            using (var tran = da.Database.BeginTransaction())
            {
                try
                {
                    TaiKhoan c = da.TaiKhoans.First(s => s.MaTk == id);
                    da.TaiKhoans.Remove(c);
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
