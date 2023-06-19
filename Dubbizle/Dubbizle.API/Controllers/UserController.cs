﻿using Dubbizle.DTOs;
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
        public async Task<ResultDTO> GetEmail(UserDTO userDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await userManager.FindByEmailAsync(userDTO.Email);
                if (applicationUser != null)
                {
                    resultDTO.StatusCode = 200;
                    return resultDTO;
                }

                else
                {
                    resultDTO.StatusCode = 204;
                    return resultDTO;
                }

            }
            resultDTO.StatusCode = 404;
            resultDTO.Data = ModelState;
            return resultDTO;
        }

        //Alzhraa
        [HttpPost("RegisterWithEmailAndPass")]
        public async Task<ResultDTO> RegisterWithEmailAndPass(UserDTO userDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = new ApplicationUser();
                applicationUser.Email = userDTO.Email;
                Random rnd = new Random();
                applicationUser.UserName = $"{"User"}{rnd.Next(1,1000)}";
                IdentityResult result = await userManager.CreateAsync(applicationUser, userDTO.Password);
                if (result.Succeeded)
                {
                    resultDTO.StatusCode=200;
                    // await userManager.AddToRoleAsync(applicationUser, "User");//insert row UserRole
                    return resultDTO;
                }
                else
                {
                    resultDTO.StatusCode = 404;
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("ModelStateErrors", error.Description);
                    }
                    resultDTO.Data = ModelState;
                    return resultDTO;
                }

            }
            resultDTO.StatusCode = 404;
            resultDTO.Data = ModelState;
            return resultDTO;
        }

        //Alzhraa
        [HttpPost("LoginWithEmailAndPass")]
        public async Task<ResultDTO> LoginWithEmailAndPass(UserDTO userDTO)
        {
            ResultDTO resultDTO = new ResultDTO();

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
                    resultDTO.StatusCode = 200;
                    resultDTO.Data = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = mytoken.ValidTo,
                        applicationUserID = applicationUser.Id
                    };
                    return resultDTO;

                }
                resultDTO.StatusCode = 404;
                resultDTO.Message = "Invalid Password";
                return resultDTO;
            }
            resultDTO.StatusCode = 404;
            resultDTO.Data = ModelState;
            return resultDTO;
        }

    


    }
}
