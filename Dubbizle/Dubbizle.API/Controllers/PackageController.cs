using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        PackageServise _PackageServise;

        public PackageController(PackageServise PackageServise)
        {
            _PackageServise= PackageServise;
        }


        [HttpGet("GetAllPackageBySubCategoryID")]
        public ResultDTO GetAllPackageBySubCategoryID(int SubCategoryID)
        {
           ResultDTO resultDTO = new ResultDTO();
            resultDTO.Data = _PackageServise.GetAllBySubCategoryID(SubCategoryID);
            resultDTO.StatusCode= 200;
            return resultDTO;
        }


        [HttpPost("PostApplicationUser_Package")]
        public ResultDTO PostApplicationUser_Package(PackageAppUserDTO packageAppUserDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            _PackageServise.PostApplicationUser_Package(packageAppUserDTO);
            resultDTO.Data = "Payment completed successfully";
            resultDTO.StatusCode = 200;
            return resultDTO;
        }



    }
}
