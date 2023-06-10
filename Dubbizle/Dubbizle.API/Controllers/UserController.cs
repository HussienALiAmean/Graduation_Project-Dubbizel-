using Dubbizle.DTOs;
using Dubbizle.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly SignInManager<ApplicationUser> signInManager;
        public UserController(UserManager<ApplicationUser> userManager, IConfiguration config, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.config = config;
            this.signInManager = signInManager;
        }

        //Alzhraa
        [HttpPost("GetEmail")]
        public async Task<IActionResult> GetEmail(UserDTO userDTO)
        {
            ApplicationUser applicationUser = await userManager.FindByEmailAsync(userDTO.Email);
            if (applicationUser != null)
                return Ok();
            else
                return BadRequest();

        }

        //Alzhraa
        [HttpPost("RegisterWithEmailAndPass")]
        public async Task<IActionResult> RegisterWithEmailAndPass(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.Email = userDTO.Email;
                applicationUser.UserName = $"{"User"}{Guid.NewGuid()}";
                IdentityResult result = await userManager.CreateAsync(applicationUser, userDTO.Password);
                if (result.Succeeded)
                {
                    // await userManager.AddToRoleAsync(applicationUser, "User");//insert row UserRole
                    return Ok("Created Success");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("ModelStateErrors", error.Description);
                    }
                    return BadRequest(ModelState);
                }

            }
            return BadRequest(ModelState);

        }

        //Alzhraa
        [HttpPost("LoginWithEmailAndPass")]
        public async Task<IActionResult> LoginWithEmailAndPass(UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await userManager.FindByEmailAsync(userDTO.Email);
                if (applicationUser != null && await userManager.CheckPasswordAsync(applicationUser, userDTO.Password))
                {
                    List<Claim> UserClaims = new List<Claim>();
                    UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, applicationUser.Id));
                    UserClaims.Add(new Claim(ClaimTypes.Name, applicationUser.UserName));
                    UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                    List<string> roles = (List<string>)await userManager.GetRolesAsync(applicationUser);

                    if (roles != null)
                    {
                        foreach (var item in roles)
                        {
                            UserClaims.Add(new Claim(ClaimTypes.Role, item));
                        }
                    }
                    var authSecritKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:SecrytKey"]));//asdZXCZX!#!@342352

                    SigningCredentials credentials =
                        new SigningCredentials(authSecritKey, SecurityAlgorithms.HmacSha256);

                    // Represent Token
                    JwtSecurityToken mytoken = new JwtSecurityToken(
                        issuer: config["JWT:ValidIss"],
                        audience: config["JWT:ValidAud"],
                        expires: DateTime.Now.AddDays(1),
                        claims: UserClaims,
                        signingCredentials: credentials
                        );

                    // Create Token
                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = mytoken.ValidTo,
                        applicationUserID = applicationUser.Id
                    });

                }

                return BadRequest("Invalid Information");
            }
            return BadRequest(ModelState);
        }

    


    }
}
