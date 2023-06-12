using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.DTOs;
using Dubbizle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.Services
{
    public class AdvertismentService
    {
        IRepository<Advertisment> _repository;
        IMapper _mapper;
        UnitOfWork unitOfWork;
        public AdvertismentService(IRepository<Advertisment> repository, IMapper mapper, UnitOfWork _unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            unitOfWork = _unitOfWork;
        }

        public IEnumerable<Advertisment> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public IEnumerable<AdvertismentDTO> GetAdvertismentsAll(Expression<Func<Advertisment, bool>> expression)
        {
            var categories = _repository.Get(expression);
            return categories.ProjectTo<AdvertismentDTO>(_mapper.ConfigurationProvider);
        }
    }
}
