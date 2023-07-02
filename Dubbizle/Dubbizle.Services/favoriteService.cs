using AutoMapper;
using Dubbizle.Data;
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
    public class FavoriteService
    {
        IRepository<Favorite> _repository;
        Context _context;
        public FavoriteService(Context context,IRepository<Favorite> repository) {
            _repository = repository;
           // _mapper = mapper;
            _context = context;
        }
        public void Add(Favorite advertisment)
        {
           _repository.Add(advertisment);
            _repository.SaveChanges();
        }
        public IEnumerable<Favorite> GetAll()
        {
            return _repository.GetAll().ToList();
        }
        public IEnumerable<Favorite> GetAll(string property1,string userId)
        {
            return _repository.GetAll(property1).Where(f=>f.ApplicationUserId==userId).ToList();

            return _repository.GetAll(property1).Where(f => f.ApplicationUserId == userId && f.Deleted == false).ToList();
        }
        public IEnumerable<Favorite> GetAllByUserId(string userId)
        {
            return _repository.Get(f => f.ApplicationUserId == userId && f.Deleted == false).ToList();
        }
        public void AddFavorit(FavoriteDTO favoriteDTO)
        {
            //return Ok(favoriteDTO);
            Favorite favorite = new Favorite();
            //Favorite favorite1 =new Favorite();
            favorite.AdvertismentID = favoriteDTO.AdvertismentID;
            favorite.ApplicationUserId = favoriteDTO.ApplicationUserId;
            //_Context.Add(favorite);
            _repository.Add(favorite);
            _repository.SaveChanges();
        }
        public void DeleteFavourite(int id, string userId)
        {
            //List<Favorite> favorites = (List<Favorite>)_repository.GetAll();
            //Favorite favorite=favorites.FirstOrDefault(f=>f.AdvertismentID==id);
            //_repository.delete(favorite);
            //_repository.SaveChanges();


            Favorite favorite = (Favorite)_repository
                .Get(f => f.AdvertismentID == id 
                && f.ApplicationUserId == userId).FirstOrDefault();
            _repository.delete(favorite);
            _repository.SaveChanges();

        }

    }
}
