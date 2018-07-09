using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMeat.API.Helpers;
using SMeat.DAL.Abstract;
using SMeat.MODELS.DTO;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace SMeat.API.Controllers
{
  [Authorize]
  [Route("api/[controller]")]
  public class UploadController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHostingEnvironment _env;

    private readonly IMapper _mapper;

    public UploadController(IUnitOfWork unitOfWork, IMapper mapper, IHostingEnvironment env)
    {
      _unitOfWork = unitOfWork;
      _env = env;

      _mapper = mapper;
    }

    [HttpPost]
    [Route("avatar")]
    public async Task<IActionResult> UploadUserImage(IFormFile file)
    {
      ImageSaver ims = new ImageSaver();

      var id = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sid)?.Value;

      var user = await _unitOfWork.UsersRepository.FirstOrDefaultAsync(p => p.Id == id.ToString());

      var filePath = ims.SaveImage(file, _env);

      user.PictureUrl = filePath;

      await _unitOfWork.UserManager.UpdateAsync(user);
      await _unitOfWork.Save();

      return Ok(_mapper.Map<UserDTO>(user));
    }

    [HttpPost]
    [Route("file")]
    public IActionResult UploadImage(IFormFile file)
    {
      ImageSaver ims = new ImageSaver();

      var filePath = ims.SaveImage(file, _env);

      if (filePath != null)
      {
        return Ok(new {Path = filePath });
      }
      else
        return BadRequest("Error on image load!");

    }
  }
}
