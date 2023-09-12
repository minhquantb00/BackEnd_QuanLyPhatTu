using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuanLyPhatTu_API.Payloads.DTOs;
using QuanLyPhatTu_API.Payloads.Requests.PhatTuRequest;
using QuanLyPhatTu_API.Service.Interfaces;

namespace QuanLyPhatTu_API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        public AuthController(IConfiguration configuration, IAuthService authService)
        {
            _configuration = configuration;
            _authService = authService;
        }
        [HttpPost]
        [Route("/api/auth/register")]
        public async Task<IActionResult> DangKy([FromForm] Request_DangKy register)
        {
            var result = await _authService.DangKyPhatTu(register);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        [Route(("/api/auth/login"))]
        public async Task<IActionResult> DangNhap(Request_DangNhap request)
        {
            var result = await _authService.DangNhap(request);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("/api/auth/renew-token")]
        public IActionResult RenewToken(TokenDTO token)
        {
            var result = _authService.RenewAccessToken(token);
            if (result == null)
            {
                return Unauthorized(result);
            }
            return Ok(result);
        }

        
        [HttpPut]
        [Route("/api/auth/change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DoiMatKhau([FromBody] Request_DoiMatKhau request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (!int.TryParse(HttpContext.User.FindFirst("Id")?.Value, out int id))
                {
                    return BadRequest("Id người dùng không hợp lệ");
                }

                var result = await _authService.DoiMatKhau(id, request);

                if (result.ToLower().Contains("Đổi mật khẩu thành công".ToLower()))
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/auth/forgot-password")]
        public async Task<IActionResult> XacNhanQuenMatKhau(Request_XacNhanQuenMatKhau request)
        {
            return Ok(await _authService.XacNhanQuenMatKhau(request));
        }

        [HttpPost]
        [Route("/api/auth/create-new-password")]
        public async Task<IActionResult> TaoMatKhauMoi(Request_TaoMatKhauMoi request)
        {
            return Ok(await _authService.TaoMatKhauMoi(request));
        }
    }
}
