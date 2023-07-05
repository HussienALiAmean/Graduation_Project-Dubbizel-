using Dubbizle.DTOs;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;

using Dubbizle.Models;
using Dubbizle.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Dubbizle.API.Helper;
using Dubbizle.Data.Repository;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dubbizle.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertismentController : ControllerBase
    {
        AdvertismentService advertismentService;
        AdvertismentFilterValueService AdvertismentFilterValueService;
        UserManager<ApplicationUser> userManager;
        SubCategory_FilterService subCategory_FilterService;
        FilterService filterService;
        FilterValueService filterValueService;
        IRepository<FiltrationValue> repository;
        IRepository<Advertisment> repositoryAds;
        FavoriteService favoriteService;
        CategoryServise categoryServise;
        IRepository<Review> reviewrepository;
        public AdvertismentController(IRepository<Review> _reviewrepository, CategoryServise _categoryServise,
            FavoriteService _favoriteService, IRepository<Advertisment> _repositoryAds,
            IRepository<FiltrationValue> _repository, FilterValueService _FilterValueService,
            FilterService _filterService, SubCategory_FilterService _subCategory_FilterService,
            AdvertismentFilterValueService _AdvertismentFilterValueService, UserManager<ApplicationUser> _userManager,
            AdvertismentFilterValueService _advertismentFilterValueService, AdvertismentService _advertismentService,
            AdvertismentImageService _advertismentImageService)
        {
            advertismentService = _advertismentService;
            userManager = _userManager;
            AdvertismentFilterValueService = _advertismentFilterValueService;
            subCategory_FilterService = _subCategory_FilterService;
            filterService = _filterService;
            filterValueService = _FilterValueService;
            repository = _repository;
            repositoryAds = _repositoryAds;
            favoriteService = _favoriteService;
            categoryServise = _categoryServise;
            reviewrepository = _reviewrepository;
        }

        [HttpGet("GetAllAdvertismentByCategory/categoryId")]
        public IEnumerable<AdvertismentHomePageDTO> GetAllAdvertismentByCategory(int categoryId)
        {
            var ads = advertismentService.GetAdvertismentsAll(c => c.CategoryID == categoryId);
            return ads;
        }



        // Alzhraa & Hussien
        [HttpGet("subCategoryID")]
        public async Task<IActionResult> GetAllBySubCategoryID(int subCategoryID, string UserId)
        {
            ResultDTO resultDTO = new ResultDTO();
            List<Advertisment> advertisments = (List<Advertisment>)advertismentService.GetAllBySubCategoryID("Advertisment_FiltrationValuesList.filtrationValue", "AdvertismentImagesList", "Advertisment_RentOptionList", subCategoryID);
            bool IsSaved = false;
            List<Favorite> favorites = (List<Favorite>)favoriteService.GetAllByUserId(UserId);

            List<AdvertismentDTO> advertismentDTOs = new List<AdvertismentDTO>();
            AdvertismentDTO advertismentDTO;

            foreach (Advertisment ad in advertisments)
            {
                advertismentDTO = new AdvertismentDTO();
                advertismentDTO.ID = ad.ID;
                advertismentDTO.Title = ad.Title;
                advertismentDTO.CategoryID = ad.CategoryID;
                advertismentDTO.SubCategoryID = ad.SubCategoryID;
                advertismentDTO.AdType = ad.AdType;
                advertismentDTO.AdStatus = ad.AdStatus;
                advertismentDTO.Location = ad.Location;
                advertismentDTO.Date = ad.Date;
                advertismentDTO.ExpirationDate = ad.ExpirationDate;
                advertismentDTO.ExpireDateOfPremium = ad.ExpireDateOfPremium;

                if (ad.ExpireDateOfPremium > DateTime.Now)
                    advertismentDTO.IsPremium = true;
                else
                    advertismentDTO.IsPremium = false;



                advertismentDTO.Advertisment_FiltrationValuesList = ad.Advertisment_FiltrationValuesList
                    .Select(item => new filterValuKey() { id = item.filtrationValue.SubCategory_FilterID, filtervalue = item.filtrationValue.Value })
                    .ToList();


                advertismentDTO.AdvertismentImagesList = ad.AdvertismentImagesList.Select(item => item.ImageName).ToList();

                IsSaved = false;
                foreach (Favorite favorite in favorites)
                {
                    if (favorite.AdvertismentID == ad.ID)
                    {
                        IsSaved = true;
                    }
                }
                advertismentDTO.IsSaved = IsSaved;
                advertismentDTO.ApplicationUserId = ad.ApplicationUserId;

                advertismentDTOs.Add(advertismentDTO);
            }

            resultDTO.StatusCode = 200;
            resultDTO.Data = advertismentDTOs;
            return Ok(resultDTO);
        }



        //Alzhraa & Hussien
        [HttpGet("CategoryID")]
        public async Task<IActionResult> GetAllByCategoryID(int CategoryID, string UserId)
        {
            ResultDTO resultDTO = new ResultDTO();
            List<Advertisment> advertisments = (List<Advertisment>)advertismentService.GetAllByCategoryID("Advertisment_FiltrationValuesList.filtrationValue", "AdvertismentImagesList", CategoryID);
            bool IsSaved = false;
            List<Favorite> favorites = (List<Favorite>)favoriteService.GetAllByUserId(UserId);

            List<AdvertismentDTO> advertismentDTOs = new List<AdvertismentDTO>();
            AdvertismentDTO advertismentDTO;

            foreach (Advertisment ad in advertisments)
            {
                advertismentDTO = new AdvertismentDTO();
                advertismentDTO.ID = ad.ID;
                advertismentDTO.Title = ad.Title;
                advertismentDTO.CategoryID = ad.CategoryID;
                advertismentDTO.SubCategoryID = ad.SubCategoryID;
                advertismentDTO.AdType = ad.AdType;
                advertismentDTO.AdStatus = ad.AdStatus;
                advertismentDTO.Location = ad.Location;
                advertismentDTO.Date = ad.Date;
                advertismentDTO.ExpirationDate = ad.ExpirationDate;
                advertismentDTO.ExpireDateOfPremium = ad.ExpireDateOfPremium;
                if (ad.ExpireDateOfPremium > DateTime.Now)
                    advertismentDTO.IsPremium = true;
                else
                    advertismentDTO.IsPremium = false;
                advertismentDTO.Advertisment_FiltrationValuesList = new List<filterValuKey>();
                foreach (Advertisment_FiltrationValue item in ad.Advertisment_FiltrationValuesList)
                {
                    advertismentDTO.Advertisment_FiltrationValuesList.Add(new filterValuKey()
                    {
                        filtervalue = item.filtrationValue.Value
                        ,
                        id = item.filtrationValue.SubCategory_FilterID
                    });
                }
                advertismentDTO.AdvertismentImagesList = new List<string>();
                foreach (AdvertismentImage item in ad.AdvertismentImagesList)
                {
                    advertismentDTO.AdvertismentImagesList.Add(item.ImageName);
                }
                IsSaved = false;
                foreach (Favorite favorite in favorites)
                {
                    if (favorite.AdvertismentID == ad.ID)
                    {
                        IsSaved = true;
                    }
                }
                advertismentDTO.IsSaved = IsSaved;
                advertismentDTO.ApplicationUserId = ad.ApplicationUserId;
                advertismentDTOs.Add(advertismentDTO);
            }

            resultDTO.StatusCode = 200;
            resultDTO.Data = advertismentDTOs;
            return Ok(resultDTO);
        }



        //Alzhraa & Hussien
        [HttpPost("filtrations")]
        public async Task<ResultDTO> GetAllByFiltrations(string location, [FromBody] params string[] FilterationArray)
        {
            ResultDTO resultDTO = new ResultDTO();
            Dictionary<int, string> filterationtable = new Dictionary<int, string>();
            foreach (string filtervalue in FilterationArray)
            {
                string[] filteratiosting = filtervalue.Split(':');
                filterationtable.Add(int.Parse(filteratiosting[0]), filteratiosting[1]);
            }
            List<Advertisment> advertisments = (List<Advertisment>)advertismentService.GetAllByFielteration(location, filterationtable).ToList();

            resultDTO.StatusCode = 200;
            resultDTO.Data = advertisments;
            return resultDTO;
        }



        // hager
        [HttpGet("Details/{AdID:int}/{UserId}")]
        public async Task<IActionResult> getDetails(int AdID, string UserId)
        {
            bool IsSaved = false;
            List<Advertisment> advertisments = (List<Advertisment>)advertismentService.GetAllAdvertisments("AdvertismentImagesList", "Advertisment_FiltrationValuesList.filtrationValue", "Advertisment_RentOptionList", "ReviewsList");
            Advertisment advertisment = advertisments.FirstOrDefault(a => a.ID == AdID);
            ApplicationUser user = await userManager.FindByIdAsync(advertisment.ApplicationUserId);
            List<Favorite> favorites = (List<Favorite>)favoriteService.GetAll();
            foreach (Favorite favorite in favorites)
            {
                if (favorite.AdvertismentID == AdID && favorite.ApplicationUserId == UserId)
                {
                    IsSaved = true;
                }
            }
            AdvertismentDetailsDTO advertismentDetailsDTO = new AdvertismentDetailsDTO();
            advertismentDetailsDTO.Id = AdID;
            advertismentDetailsDTO.AdStatus = advertisment.AdStatus;
            advertismentDetailsDTO.Title = advertisment.Title;
            advertismentDetailsDTO.AdType = advertisment.AdType;
            advertismentDetailsDTO.ApplicationUserId = user.Id;
            advertismentDetailsDTO.ApplicationUserName = user.UserName;
            advertismentDetailsDTO.ApplicationEmail = user.Email;
            advertismentDetailsDTO.Date = advertisment.Date;
            advertismentDetailsDTO.ExpirationDate = advertisment.ExpirationDate;
            advertismentDetailsDTO.IsSaved = IsSaved;
            advertismentDetailsDTO.ExpireDateOfPremium = advertisment.ExpireDateOfPremium;
            advertismentDetailsDTO.Location = advertisment.Location;
            List<SubCategory_Filter> s = (List<SubCategory_Filter>)subCategory_FilterService.GetAllBySubCategory("Filter");





            foreach (Advertisment_FiltrationValue filter in advertisment.Advertisment_FiltrationValuesList)
            {
                FiltrationValue fil = repository.GetByID(filter.FiltrationValueID);
                SubCategory_Filter sub = s.FirstOrDefault(s => s.ID == fil.SubCategory_FilterID);


                AdvertismentFilterValueDTO advertismentValueDTO = new AdvertismentFilterValueDTO();
                advertismentValueDTO.Value = filter.filtrationValue.Value;
                advertismentValueDTO.Key = sub.Filter.Name;
                advertismentDetailsDTO.Advertisment_FiltrationValuesList.Add(advertismentValueDTO);
            }
            foreach (AdvertismentImage image in advertisment.AdvertismentImagesList)
            {
                AdvertismentImageDTO advertismentImageDTO = new AdvertismentImageDTO();
                advertismentImageDTO.ImageName = image.ImageName;
                advertismentDetailsDTO.AdvertismentImagesList.Add(advertismentImageDTO);
            }
            foreach (Advertisment_RentOption rentOption in advertisment.Advertisment_RentOptionList)
            {
                AdvertismentRentOptionDTO advertismentRentDTO = new AdvertismentRentOptionDTO();
                advertismentRentDTO.Cost = rentOption.Cost;
                advertismentDetailsDTO.Advertisment_RentOptionList.Add(advertismentRentDTO);

            }
            //Review review = reviewrepository.GetAll().FirstOrDefault(d => d.Deleted == false);
            advertisment.ReviewsList = advertisment.ReviewsList.Where(r => r.Deleted == false).ToList();
            foreach (Review review in advertisment.ReviewsList)
            {
                ReviewDto reviewDTO = new ReviewDto();
                ApplicationUser applicationUser = await userManager.FindByIdAsync(review.AutherId);
                //Review rv = r.FirstOrDefault(r=>r.ID==review.ID);
                reviewDTO.text = review.Text;

                reviewDTO.Rate = review.Rate;
                reviewDTO.userName = applicationUser.UserName;
                reviewDTO.AutherId = review.AutherId;
                reviewDTO.AdvertismentID = review.AdvertismentID;
                reviewDTO.ID = review.ID;

                reviewDTO.Rate = review.Rate;
                reviewDTO.userName = applicationUser.UserName;
                reviewDTO.AutherId = review.AutherId;
                reviewDTO.AdvertismentID = review.AdvertismentID;
                reviewDTO.ID = review.ID;


                advertismentDetailsDTO.ReviewsList.Add(reviewDTO);
            }

            return Ok(advertismentDetailsDTO);
        }

        //hager
        [HttpGet("Advertisment's User/{UserId}/{currentUserId}")]
        public async Task<IActionResult> GetAdvertisments_User(string UserId, string currentUserId)
        {
            bool IsSaved = false;
            List<Advertisment> advertisments = (List<Advertisment>)advertismentService.GetAdvertismentUsers(UserId, "AdvertismentImagesList", "Advertisment_RentOptionList");
            ApplicationUser applicationUser = await userManager.FindByIdAsync(UserId);
            Advertisments_s_User ad = new Advertisments_s_User();
            ad.UserName = applicationUser.UserName;
            ad.UserEmail = applicationUser.Email;
            List<Favorite> favorites = (List<Favorite>)favoriteService.GetAllByUserId(currentUserId);
            foreach (Advertisment advertisment in advertisments)
            {
                AdvertismetsUsersDTO ad1 = new AdvertismetsUsersDTO();
                ad1.Id = advertisment.ID;
                ad1.AdType = advertisment.AdType;
                ad1.ExpireDateOfPremium = advertisment.ExpireDateOfPremium;
                ad1.AdStatus = advertisment.AdStatus;
                ad1.Date = advertisment.Date;
                ad1.ExpirationDate = advertisment.ExpirationDate;
                ad1.Location = advertisment.Location;
                ad1.Title = advertisment.Title;
                ad.CountAds++;
                ad.advertismetsUsersDTOs.Add(ad1);
                IsSaved = false;
                foreach (Favorite favorite in favorites)
                {
                    if (favorite.AdvertismentID == advertisment.ID)
                    {
                        IsSaved = true;
                    }
                }
                ad1.isSaved = IsSaved;
                foreach (AdvertismentImage advertismentImage in advertisment.AdvertismentImagesList)
                {
                    AdvertismentImageDTO advertismentImageDTO = new AdvertismentImageDTO();
                    advertismentImageDTO.ImageName = advertismentImage.ImageName;
                    ad1.imageDTOs.Add(advertismentImageDTO);
                }
                foreach (Advertisment_RentOption advertismentRent in advertisment.Advertisment_RentOptionList)
                {
                    AdvertismentRentOptionDTO advertismentRentDTO = new AdvertismentRentOptionDTO();
                    advertismentRentDTO.Cost = advertismentRent.Cost;
                    ad1.Advertisment_RentOptionList.Add(advertismentRentDTO);
                }
                IsSaved = false;
            }
            return Ok(ad);
        }

        [HttpGet("GetAllAdsInHomePage/{userId}")]
        public IActionResult GetAllAdvertismentInHomePage(string userId)
        {
            Boolean isSaved1 = false;
            List<Category> categories = (List<Category>)categoryServise.GetAll("CategoryAdvertismentsList");
            //List<Advertisment> advertisments =(List<Advertisment>) advertismentService.GetAllAdvertisments("AdvertismentImagesList", "Advertisment_RentOptionList");
            List<CategoryWithAdvertismentDTO> categoryWithAdvertisment = new List<CategoryWithAdvertismentDTO>();
            List<Favorite> favorites = (List<Favorite>)favoriteService.GetAllByUserId(userId);
            foreach (Category category in categories)
            {
                CategoryWithAdvertismentDTO category1 = new CategoryWithAdvertismentDTO();
                category1.Name = category.Name;
                category1.ID=category.ID;
                List<Advertisment> advertisments = (List<Advertisment>)advertismentService.GetAllAdvertisments("AdvertismentImagesList", "Advertisment_RentOptionList", category.ID);
                foreach (Advertisment advertisment in advertisments)
                {
                    foreach (Favorite favorite in favorites)
                    {
                        if (advertisment.ID == favorite.AdvertismentID)
                        {
                            isSaved1 = true;
                        }
                    }
                    AdvertismentHomePageDTO advertismentHomePageDTO = new AdvertismentHomePageDTO();
                    advertismentHomePageDTO.AdType = advertisment.AdType;
                    advertismentHomePageDTO.AdStatus = advertisment.AdStatus;
                    advertismentHomePageDTO.Title = advertisment.Title;
                    advertismentHomePageDTO.Date = advertisment.Date;
                    advertismentHomePageDTO.Location = advertisment.Location;
                    advertismentHomePageDTO.Id = advertisment.ID;
                    advertismentHomePageDTO.IsSaved = isSaved1;
                    foreach (AdvertismentImage advertismentImage in advertisment.AdvertismentImagesList)
                    {
                        AdvertismentImageDTO advertismentImageDTO = new AdvertismentImageDTO();
                        advertismentImageDTO.ImageName = advertismentImage.ImageName;
                        advertismentHomePageDTO.AdvertismentImagesList.Add(advertismentImageDTO);
                    }
                    foreach (Advertisment_RentOption advertismentRent in advertisment.Advertisment_RentOptionList)
                    {
                        AdvertismentRentOptionDTO advertismentRentDTO = new AdvertismentRentOptionDTO();
                        advertismentRentDTO.Cost = advertismentRent.Cost;
                        advertismentHomePageDTO.Advertisment_RentOptionList.Add(advertismentRentDTO);
                    }
                    category1.CategoryAdvertismentsList.Add(advertismentHomePageDTO);
                    isSaved1 = false;
                }
                categoryWithAdvertisment.Add(category1);
            }
            return Ok(categoryWithAdvertisment);
        }



        // Alzhraa 
        [HttpGet("GetMyAdvertisments")]
        public ResultDTO GetMyAdvertisments(string ApplicationUserID)
        {
            List<Advertisment> advertisments = (List<Advertisment>)advertismentService.GetMyAdvertisments(ApplicationUserID);
            List<AdvertismentDTO> advertismentDTOs = new List<AdvertismentDTO>();
            AdvertismentDTO advertismentDTO;
            foreach (Advertisment ad in advertisments)
            {
                advertismentDTO = new AdvertismentDTO();
                advertismentDTO.ID = ad.ID;
                advertismentDTO.Title = ad.Title;
                advertismentDTO.AdStatus = ad.AdStatus;
                advertismentDTO.Date = ad.Date;
                advertismentDTO.ExpirationDate = ad.ExpirationDate;
                advertismentDTO.CategoryID = ad.CategoryID;
                advertismentDTO.SubCategoryID = ad.SubCategoryID;
                advertismentDTO.AdvertismentImagesList = new List<string>();
                advertismentDTO.AdvertismentImagesList.Add(ad.AdvertismentImagesList[0].ImageName);
                advertismentDTOs.Add(advertismentDTO);
            }
            ResultDTO resultDTO = new ResultDTO();
            resultDTO.Data = advertismentDTOs;
            resultDTO.StatusCode = 200;
            return resultDTO;
        }


        [HttpGet("search")]
        public IActionResult Search(string query, string UserId)
        {
            ResultDTO resultDTO = new ResultDTO();
            List<Advertisment> advertisments = (List<Advertisment>)advertismentService.GetBySearchQuery(query, "Category", "SubCategory", "AdvertismentImagesList", "Advertisment_FiltrationValuesList.filtrationValue");
            bool IsSaved = false;
            List<Favorite> favorites = (List<Favorite>)favoriteService.GetAllByUserId(UserId);

            List<AdvertismentDTO> advertismentDTOs = new List<AdvertismentDTO>();
            AdvertismentDTO advertismentDTO;

            foreach (Advertisment ad in advertisments)
            {
                advertismentDTO = new AdvertismentDTO();
                advertismentDTO.ID = ad.ID;
                advertismentDTO.Title = ad.Title;
                advertismentDTO.CategoryID = ad.CategoryID;
                advertismentDTO.SubCategoryID = ad.SubCategoryID;
                advertismentDTO.AdType = ad.AdType;
                advertismentDTO.AdStatus = ad.AdStatus;
                advertismentDTO.Location = ad.Location;
                advertismentDTO.Date = ad.Date;
                advertismentDTO.ExpirationDate = ad.ExpirationDate;
                advertismentDTO.ExpireDateOfPremium = ad.ExpireDateOfPremium;
                if (ad.ExpireDateOfPremium > DateTime.Now)
                    advertismentDTO.IsPremium = true;
                else
                    advertismentDTO.IsPremium = false;
                advertismentDTO.Advertisment_FiltrationValuesList = new List<filterValuKey>();
                foreach (Advertisment_FiltrationValue item in ad.Advertisment_FiltrationValuesList)
                {
                    advertismentDTO.Advertisment_FiltrationValuesList.Add(new filterValuKey()
                    {
                        filtervalue = item.filtrationValue.Value
                        ,
                        id = item.filtrationValue.SubCategory_FilterID
                    });
                }
                advertismentDTO.AdvertismentImagesList = new List<string>();
                foreach (AdvertismentImage item in ad.AdvertismentImagesList)
                {
                    advertismentDTO.AdvertismentImagesList.Add(item.ImageName);
                }
                IsSaved = false;
                foreach (Favorite favorite in favorites)
                {
                    if (favorite.AdvertismentID == ad.ID)
                    {
                        IsSaved = true;
                    }
                }
                advertismentDTO.IsSaved = IsSaved;
                advertismentDTO.ApplicationUserId = ad.ApplicationUserId;
                advertismentDTOs.Add(advertismentDTO);
            }

            resultDTO.StatusCode = 200;
            resultDTO.Data = advertismentDTOs;
            return Ok(resultDTO);
        }

    }
}