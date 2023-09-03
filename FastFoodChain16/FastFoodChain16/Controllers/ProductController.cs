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
    }
}
