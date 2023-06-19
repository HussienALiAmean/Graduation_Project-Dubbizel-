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
    public class AdvertismentFilterValueService
    {
        IRepository<Advertisment_FiltrationValue> _repository;
        IMapper _mapper;
        UnitOfWork unitOfWork;

        public void Add(Advertisment_FiltrationValue advertisment)
        {
            _repository.Add(advertisment);
            unitOfWork.SaveChanges();
        }
        public Advertisment_FiltrationValue GetByID(int id)
        {
            return _repository.GetByID(id);
        }
    }
}
