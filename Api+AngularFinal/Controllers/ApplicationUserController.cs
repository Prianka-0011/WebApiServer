using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api_AngularFinal.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api_AngularFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationUserController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettions _appsettings;
        public ApplicationUserController(UserManager<ApplicationUser>userManager, SignInManager<ApplicationUser>signInManager, IOptions<ApplicationSettions> appSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appsettings = appSettings.Value;
        }

        [HttpPost]
        [Route("Register")]
        //POST: /api/ApplicationUser/Register
        public async Task<Object>PostApplicationUser(ApplicationUserModel model)
        {
            var applicationUser = new ApplicationUser()
            {
                UserName=model.UserName,
                Email=model.Email,
                FullName=model.FullName
            };
            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                return Ok(result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        [Route("Login")]
        //POST: /api/ApplicationUser/Login
        public async Task<IActionResult>Login(Login model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if(user!=null && await _userManager.CheckPasswordAsync(user,model.Password))
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appsettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                    
                };
                var tokenHandlar = new JwtSecurityTokenHandler();
                var securityToken = tokenHandlar.CreateToken(tokenDescriptor);
                var token = tokenHandlar.WriteToken(securityToken);
                return Ok(new { token});
            }
            else
            {
                return BadRequest(new { Message = "UserName or Password Incorrect" });
            }

        }


    }
    
}