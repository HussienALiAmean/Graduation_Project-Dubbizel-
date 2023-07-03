using AutoMapper;
using AutoMapper.QueryableExtensions;
using Dubbizle.Data.Repository;
using Dubbizle.Data.UnitOfWork;
using Dubbizle.DTOs;
using Dubbizle.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Dubbizle.Services
{
    public class AdUserService
    {
        IRepository<Advertisment> _advertismentRepository;


        public AdUserService(IRepository<Advertisment> advertismentRepository)
        {
          
            _advertismentRepository= advertismentRepository;
            
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


    }
}
