using AutoMapper;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.Services
{
    public class AdvertismentImageService
    {
        IRepository<AdvertismentImage> _repository;
        IMapper _mapper;
        UnitOfWork unitOfWork;

        public void Add(AdvertismentImage advertisment)
        {
            _repository.Add(advertisment);
            _repository.SaveChanges();
        }
    }
}
