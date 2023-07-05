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
    public class FilterService
    {
        IRepository<Filter> _repository;
        IMapper _mapper;
        UnitOfWork unitOfWork;
        public Filter GetByID(int id)
        {
            return _repository.GetByID(id);
        }
    }
}
