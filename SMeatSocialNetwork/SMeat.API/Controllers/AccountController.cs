using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SMeat.DAL;
using SMeat.MODELS.Models;
using SMeat.MODELS.Models.BindingModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SMeat.API;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace SMeatSocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IOptions<JWTOptions> _options;

        public AccountController(IUnitOfWork unitOfWork, IOptions<JWTOptions> options)
        {
            _unitOfWork = unitOfWork;
            _options = options;
        }

        // POST api/account/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginBindingModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork.UserManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    var roles = await _unitOfWork.UserManager.GetRolesAsync(user);
                    var result = await _unitOfWork.SignInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimsIdentity.DefaultNameClaimType,user.UserName),
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sid, user.Id) // Set userid to token Sid claim
                        };
                        if (roles.Any())
                        {
                            claims.AddRange(roles.Select(role => new Claim(JwtRegisteredClaimNames.Sub, role)));
                        }
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                                          issuer: _options.Value.Issuer,
                                          audience: _options.Value.Issuer,
                                          claims: claims,
                                          expires: DateTime.UtcNow.AddMinutes(30),
                                          signingCredentials: creds);

                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), id = user.Id });
                    }
                }
            }

            return BadRequest("Could not create token");
        }

        // PUT api/values/5
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]RegisterBindingModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _unitOfWork.UserManager.FindByNameAsync(model.Email) != null)
                {
                    return BadRequest("user already exists");
                }
                var user = new User { UserName = model.Email, Email = model.Email, LastName = model.LastName, Name = model.Name };
                if (user != null)
                {
                    var result = await _unitOfWork.UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                          new Claim(ClaimsIdentity.DefaultNameClaimType,user.UserName),
                          new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                          new Claim(JwtRegisteredClaimNames.Sid, user.Id) // Set userid to token Sid claim
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(_options.Value.Issuer,
                          _options.Value.Issuer,
                          claims,
                          expires: DateTime.Now.AddMinutes(30),
                          signingCredentials: creds);

                        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), id = user.Id });
                    }
                }
            }

            return BadRequest("Could not create token");
        }
    }
}
