using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandmarkRemarkModel;
using LandmarkRemarkService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LandmarkRemark.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class RemarkController : ControllerBase
    {
        private IRemarkService _remarkService;

        public RemarkController(IRemarkService remarkService)
        {
            _remarkService = remarkService;
        }

        // GET: api/User
        [HttpGet("allRemarks")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _remarkService.GetAllRemarks();
            return Ok(result);
        }

        // GET: api/User/5
        [HttpPost("remark")]
        public async Task<IActionResult> Remark([FromBody] Remark remark)
        {
            await _remarkService.RemarkLandmark(remark);
            return Ok();
        }
    }
}
