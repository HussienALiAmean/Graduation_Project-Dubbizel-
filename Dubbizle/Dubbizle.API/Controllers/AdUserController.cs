using Dubbizle.DTOs;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Dubbizle.Models;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Dubbizle.Data;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdUserController : ControllerBase
    {
        AdUserService _AdUserService;
        public AdUserController( AdUserService adUserService) {
            _AdUserService = adUserService;
         }

        [HttpPut("DeActivateMyAd")]
        public ResultDTO DeActivateMyAd(int id)
        {
            ResultDTO resultDTO = new ResultDTO();
            Advertisment advertisment = _AdUserService.GetAdvertismentByID(id);
            advertisment.AdStatus = "Moderated";
            _AdUserService.EditAdvertismentState(advertisment);
            resultDTO.StatusCode = 200;
            return resultDTO;
        }

        [HttpPut("AvtivateMyAd")]
        public ResultDTO AvtivateMyAd(int id)
        {
            ResultDTO resultDTO = new ResultDTO();
            Advertisment advertisment = _AdUserService.GetAdvertismentByID(id);
            advertisment.AdStatus = "Active";
            _AdUserService.EditAdvertismentState(advertisment);
            resultDTO.StatusCode = 200;
            return resultDTO;
        }


        [HttpPost("PostAdvertsiment")]
        public ResultDTO PostAdvertsiment([FromForm]PostAdvertismentDTO postAdvertismentDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            resultDTO.Data= _AdUserService.PostAdvertsiment(postAdvertismentDTO);
            resultDTO.StatusCode = 200;
            return resultDTO;
        }

        [HttpPut("SaveAdvertsimentEdits")]
        public ResultDTO SaveAdvertsimentEdits([FromForm] SaveAdvertismentDTO saveAdvertismentDTO)
        {
            ResultDTO resultDTO = new ResultDTO();
            resultDTO.Data = _AdUserService.SaveAdvertsimentEdits(saveAdvertismentDTO);
            resultDTO.StatusCode = 200;
            return resultDTO;
        }

        [HttpGet("GetAdvertsimentDetailsForEdit")]
        public ResultDTO GetAdvertsimentDetailsForEdit(int ID)
        {
            ResultDTO resultDTO = new ResultDTO();
            resultDTO.Data = _AdUserService.GetAdvertsimentDetailsForEdit(ID);
            resultDTO.StatusCode = 200;
            return resultDTO;
        }


        [HttpGet("GetAdChatUsers")]
        public ResultDTO GetAdChatUsers(int id)
        {
            ResultDTO resultDTO = new ResultDTO();
           resultDTO.Data= _AdUserService.GetAdChatUsers(id);
            resultDTO.StatusCode = 200;
            return resultDTO;
        }


        [HttpGet("RentMyAd")]
        public ResultDTO RentMyAd(int id, string ApplicationUserId)
        {
            ResultDTO resultDTO = new ResultDTO();
            Advertisment advertisment = _AdUserService.GetAdvertismentByID(id);
            advertisment.AdStatus = "Not Active";
            _AdUserService.EditAdvertismentState(advertisment);
            _AdUserService.RentMyAd(id,ApplicationUserId);
            resultDTO.StatusCode = 200;
            return resultDTO;
        }


    }
}
