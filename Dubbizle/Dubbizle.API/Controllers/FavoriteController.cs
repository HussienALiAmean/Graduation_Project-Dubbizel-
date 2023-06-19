﻿using Dubbizle.Data;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        FavoriteService favoriteService;
        IRepository<Favorite> favoriteRepository;
        AdvertismentService advertismentService;
        Context context;
        //UnitOfWork unitOfWork;
        public FavoriteController(Context _context,AdvertismentService _advertismentService, IRepository<Favorite> _repository,FavoriteService _favoriteService) { 
          favoriteService = _favoriteService;
            //unitOfWork = _unitOfWork;
            favoriteRepository = _repository;
            advertismentService = _advertismentService;
            context = _context;
        }
        [HttpPost("AddFavorite")]
        public IActionResult AddFavorite(FavoriteDTO favoriteDTO)
        {
            favoriteService.AddFavorit(favoriteDTO);
            return Ok(favoriteDTO);
        }

        [HttpDelete("DeleteFavourite/{id:int}/{userId}")]
        public IActionResult DeleteFavourite(int id,string userId)
        {
            List<Favorite> favorites = (List<Favorite>)favoriteService.GetAll();
            Favorite favorite = favorites.FirstOrDefault(f => f.AdvertismentID == id &&userId==f.ApplicationUserId);
            favoriteRepository.delete(favorite);
            favoriteRepository.SaveChanges();
            return Ok("deleted");
        }

        [HttpGet("GetAllFavorite/{CurrentUserId}")]
        public IActionResult GetAllFavorite(string CurrentUserId)
        {
            List<Favorite> favorites= (List<Favorite>)favoriteService.GetAll("Advertisment",CurrentUserId);
            AllFavoriteDTO allFavoriteDTO = new AllFavoriteDTO();
            foreach(Favorite favorite in favorites)
            {
                Advertisment advertisment= advertismentService.GetAdsByID(favorite.AdvertismentID, "AdvertismentImagesList", "Advertisment_RentOptionList");
                AdvertismentHomePageDTO ad=new AdvertismentHomePageDTO();
                ad.Title = advertisment.Title;
                ad.AdStatus= advertisment.AdStatus;
                ad.AdType= advertisment.AdType;
                ad.Date= advertisment.Date;
                ad.Id = advertisment.ID;
                ad.IsSaved = true;
                ad.Location = advertisment.Location;
               foreach(AdvertismentImage advertismentImage in advertisment.AdvertismentImagesList)
                {
                    AdvertismentImageDTO advertismentImageDTO = new AdvertismentImageDTO();
                    advertismentImageDTO.ImageName= advertismentImage.ImageName;
                    ad.AdvertismentImagesList.Add(advertismentImageDTO);
                }
                foreach (Advertisment_RentOption advertismentRent in advertisment.Advertisment_RentOptionList)
                {
                    AdvertismentRentOptionDTO advertismentRentDTO = new AdvertismentRentOptionDTO();
                    advertismentRentDTO.Cost = advertismentRent.Cost;
                    ad.Advertisment_RentOptionList.Add(advertismentRentDTO);
                }
                allFavoriteDTO.advertismentHomePageDTOs.Add(ad);

                
            }
            return Ok(allFavoriteDTO);
        }

       
    }
}
