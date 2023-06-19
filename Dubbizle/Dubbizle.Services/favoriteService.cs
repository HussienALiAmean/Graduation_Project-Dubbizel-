using AutoMapper;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.DTOs;
using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.Services
{
    public class favoriteService
    {
        IRepository<Favorite> _repository;
       
        public favoriteService(IRepository<Favorite> repository)
        {
            _repository = repository;
        }
        public void AddFavorit(FavorieDTO favoriteDTO)
        {
            Favorite favorite = new Favorite();
            //Favorite favorite1 =new Favorite();
            favorite.AdvertismentID = favoriteDTO.AdvertismentID;
            favorite.ApplicationUserId = favoriteDTO.ApplicationUserId;
            _repository.Add(favorite);
            _repository.SaveChanges();
        }
        public IEnumerable<Favorite> GetAll()
        {
            return _repository.GetAll().ToList();
        }
    }
}
