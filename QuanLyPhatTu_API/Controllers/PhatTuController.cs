using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhatTu_API.Service.Implements;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Controllers
{
    [ApiController]
    public class PhatTuController : ControllerBase
    {
        private readonly IPhatTuService _iPhatTuService;
        public PhatTuController()
        {
            _iPhatTuService = new PhatTuService();
        }
        [HttpGet]
        [Route("/api/phattu/get-all")]
        [Authorize(Roles = "ADMIN, MOD")]
        public async Task<IActionResult> LayTatCaPhatTu(int pageSize, int pageNumber)
        {
            return Ok(await _iPhatTuService.LayTatCaPhatTu(pageSize, pageNumber));
        }
    }
}
