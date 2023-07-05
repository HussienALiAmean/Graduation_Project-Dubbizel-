using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dubbizle.API.Helper;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;


namespace Dubbizle.Services
{
    public class AdUserService
    {
        IRepository<Advertisment> _advertismentRepository;

        IRepository<AdvertismentImage> _advertismentImageRepository;

        IRepository<Advertisment_FiltrationValue> _advertismentFiltrationValuRepository;
        IRepository<Advertisment_RentOption> _advertismentRentOptionRepository;
        IRepository<Room> _roomRepository;


        public AdUserService(IRepository<Advertisment> advertismentRepository, IRepository<AdvertismentImage> advertismentImageRepository,
           IRepository<Advertisment_FiltrationValue> advertismentFiltrationValuRepository,
           IRepository<Advertisment_RentOption> advertismentRentOptionRepository,
           IRepository<Room> roomRepository)
        {
          
            _advertismentRepository= advertismentRepository;
            _advertismentImageRepository = advertismentImageRepository;
            _advertismentFiltrationValuRepository =advertismentFiltrationValuRepository;
            _advertismentRentOptionRepository=advertismentRentOptionRepository;
            _roomRepository= roomRepository;

        }
        public Advertisment GetAdvertismentByID(int id)
        {
            return _advertismentRepository.GetByID(id);
        }
      
        public void EditAdvertismentState(Advertisment advertisment)
        {
            _advertismentRepository.Update(advertisment);
            _advertismentRepository.SaveChanges();
        }

        public Advertisment PostAdvertsiment(PostAdvertismentDTO postAdvertismentDTO)
        {
            Advertisment advertisment = new Advertisment();
            advertisment.Title= postAdvertismentDTO.Title;
            advertisment.Location= postAdvertismentDTO.Location;
            advertisment.AdType= postAdvertismentDTO.AdType;
            advertisment.AdStatus = "Pending";
            advertisment.Date=DateTime.Now;
            advertisment.ExpirationDate= DateTime.Now;
            advertisment.ExpireDateOfPremium= DateTime.Now;
            advertisment.CategoryID= postAdvertismentDTO.CategoryID;
            advertisment.SubCategoryID= postAdvertismentDTO.SubCategoryID;
            advertisment.ApplicationUserId= postAdvertismentDTO.ApplicationUserId;
            _advertismentRepository.Add(advertisment);
            _advertismentImageRepository.SaveChanges();

            Advertisment_FiltrationValue advertisment_FiltrationValue;
            List<filterValueDTo2> AdvertismentFiltrationValuesList = JsonConvert.DeserializeObject<List<filterValueDTo2>>(postAdvertismentDTO.AdvertismentFiltrationValuesList);
            foreach (var fiterValue in AdvertismentFiltrationValuesList)
            {
                advertisment_FiltrationValue = new Advertisment_FiltrationValue();
                advertisment_FiltrationValue.FiltrationValueID = fiterValue.filterValueID; ;
                advertisment_FiltrationValue.AdvertismentID = advertisment.ID;
                _advertismentFiltrationValuRepository.Add(advertisment_FiltrationValue);
                _advertismentFiltrationValuRepository.SaveChanges();
            }

            AdvertismentImage advertismentImage;
            foreach (var image in postAdvertismentDTO.AdvertismentImagesList)
            {
                advertismentImage = new AdvertismentImage();
                advertismentImage.AdvertismentID = advertisment.ID;
                advertismentImage.ImageName = ImagesHelper.UploadImg(image, "AdvertismentIMG");
                _advertismentImageRepository.Add(advertismentImage);
                _advertismentImageRepository.SaveChanges();
            }


            Advertisment_RentOption advertisment_RentOption;
            List<RentOptionDTO> AdvertismentRentOptions = JsonConvert.DeserializeObject<List<RentOptionDTO>>(postAdvertismentDTO.AdvertismentRentOptions);
            foreach (var rentOption in AdvertismentRentOptions)
            {
                advertisment_RentOption = new Advertisment_RentOption();
                advertisment_RentOption.Unit=rentOption.Unit;
                advertisment_RentOption.Duration= rentOption.Duration;
                advertisment_RentOption.Cost=rentOption.Cost;
                advertisment_RentOption.AdvertismentID = advertisment.ID;
                _advertismentRentOptionRepository.Add(advertisment_RentOption);
                _advertismentRentOptionRepository.SaveChanges();
            }

            return advertisment;
        }


        public EditAdvertismentDTO GetAdvertsimentDetailsForEdit(int ID)
        {
            Advertisment advertisment = _advertismentRepository.GetAll("Advertisment_FiltrationValuesList", "Advertisment_RentOptionList", "AdvertismentImagesList").FirstOrDefault(a=>a.ID==ID);
            EditAdvertismentDTO editAdvertismentDTO = new EditAdvertismentDTO();
            editAdvertismentDTO.ID= advertisment.ID;
            editAdvertismentDTO.Title = advertisment.Title;
            editAdvertismentDTO.Location = advertisment.Location;
            editAdvertismentDTO.AdType = advertisment.AdType;
            
            foreach(var filterValue in advertisment.Advertisment_FiltrationValuesList)
            {
                editAdvertismentDTO.AdvertismentFiltrationValuesList.Add(filterValue.FiltrationValueID);
            }

            EditRentOptionDTO editRentOptionDTO;
            foreach (var rentOption in advertisment.Advertisment_RentOptionList)
            {
                editRentOptionDTO=new EditRentOptionDTO();
                editRentOptionDTO.ID=rentOption.ID;
                editRentOptionDTO.Unit=rentOption.Unit;
                editRentOptionDTO.Duration=rentOption.Duration;
                editRentOptionDTO.Cost=rentOption.Cost;
                editAdvertismentDTO.AdvertismentRentOptions.Add(editRentOptionDTO);
            }


            ImageDTO imageDTO;
            foreach (var image in advertisment.AdvertismentImagesList)
            {
                imageDTO = new ImageDTO();
                imageDTO.ID = image.ID;
                imageDTO.Name = image.ImageName;
                editAdvertismentDTO.AdvertismentImagesList.Add(imageDTO);
            }
            return editAdvertismentDTO;
        }


        public Advertisment SaveAdvertsimentEdits(SaveAdvertismentDTO saveAdvertismentDTO)
        {
            Advertisment advertisment = GetAdvertismentByID((int)saveAdvertismentDTO.ID);
            advertisment.Title = saveAdvertismentDTO.Title;
            advertisment.Location = saveAdvertismentDTO.Location;
            _advertismentRepository.Update(advertisment);
            _advertismentImageRepository.SaveChanges();


            List<Advertisment_FiltrationValue> advertisment_FiltrationValues = _advertismentFiltrationValuRepository.GetAll(fv => fv.AdvertismentID == saveAdvertismentDTO.ID).ToList();
            int index = 0;
            Advertisment_FiltrationValue advertisment_FiltrationValue;
            List<filterValueDTo2> AdvertismentFiltrationValuesList = JsonConvert.DeserializeObject<List<filterValueDTo2>>(saveAdvertismentDTO.AdvertismentFiltrationValuesList);
            foreach (var fiterValue in AdvertismentFiltrationValuesList)
            {
                advertisment_FiltrationValue = advertisment_FiltrationValues[index];
                advertisment_FiltrationValue.FiltrationValueID = fiterValue.filterValueID; ;
                _advertismentFiltrationValuRepository.Update(advertisment_FiltrationValue);
                _advertismentFiltrationValuRepository.SaveChanges();
                index++;
            }

            AdvertismentImage advertismentImage;
            foreach (var image in saveAdvertismentDTO.AdvertismentImagesList)
            {
                advertismentImage = new AdvertismentImage();
                advertismentImage.AdvertismentID = advertisment.ID;
                advertismentImage.ImageName = ImagesHelper.UploadImg(image, "AdvertismentIMG");
                _advertismentImageRepository.Add(advertismentImage);
                _advertismentImageRepository.SaveChanges();
            }

            Advertisment_RentOption advertisment_RentOption;
            List<EditRentOptionDTO> AdvertismentRentOptions = JsonConvert.DeserializeObject<List<EditRentOptionDTO>>(saveAdvertismentDTO.AdvertismentRentOptions);
            foreach (var rentOption in AdvertismentRentOptions)
            {
                if(rentOption.ID!=0)//exist element
                {
                    advertisment_RentOption = _advertismentRentOptionRepository.GetByID((int)rentOption.ID);
                    advertisment_RentOption.Unit = rentOption.Unit;
                    advertisment_RentOption.Duration = rentOption.Duration;
                    advertisment_RentOption.Cost = rentOption.Cost;
                    advertisment_RentOption.AdvertismentID = advertisment.ID;
                    _advertismentRentOptionRepository.Update(advertisment_RentOption);
                }
                else
                {
                    advertisment_RentOption = new Advertisment_RentOption();
                    advertisment_RentOption.Unit = rentOption.Unit;
                    advertisment_RentOption.Duration = rentOption.Duration;
                    advertisment_RentOption.Cost = rentOption.Cost;
                    advertisment_RentOption.AdvertismentID = advertisment.ID;
                    _advertismentRentOptionRepository.Add(advertisment_RentOption);
                }
               
                _advertismentRentOptionRepository.SaveChanges();
            }

            AdvertismentImage deleteAdvertismentImage;
            foreach(var deletedAdImageID in saveAdvertismentDTO.DeletedImages)
            {
                deleteAdvertismentImage = _advertismentImageRepository.GetByID(deletedAdImageID);
                deleteAdvertismentImage.Deleted = true;
                _advertismentImageRepository.Update(deleteAdvertismentImage);
                _advertismentImageRepository.SaveChanges();
            }

            //Advertisment_RentOption deleteAdvertisment_RentOption;
            //foreach (var deletedAdRentOptionID in saveAdvertismentDTO.DeletedRentOptions)
            //{
            //    deleteAdvertisment_RentOption = _advertismentRentOptionRepository.GetByID(deletedAdRentOptionID);
            //    deleteAdvertisment_RentOption.Deleted = true;
            //    _advertismentRentOptionRepository.Update(deleteAdvertisment_RentOption);
            //    _advertismentRentOptionRepository.SaveChanges();
            //}

            return advertisment;
        }


        public List<ApplicationUser> GetAdChatUsers(int AdID)
        {
           List<Room> rooms=  _roomRepository.GetAll("Sender").Where(r => r.AdvertismentID == AdID).ToList() ;
            List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
            foreach (Room room in rooms)
            {
                applicationUsers.Add(room.Sender);
            }
            return applicationUsers;
        }


        public void RentMyAd(int AdID,string ApplicationUserId)
        {
            Room room = _roomRepository.GetAll("Sender").Where(r =>r.SenderId==ApplicationUserId && r.AdvertismentID == AdID).FirstOrDefault();
            room.Sold = true;
            _roomRepository.Update(room);
            _roomRepository.SaveChanges();
        }




    }
}
