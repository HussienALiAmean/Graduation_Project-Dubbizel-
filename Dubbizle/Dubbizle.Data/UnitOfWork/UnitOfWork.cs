using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dubbizle.Data.UnitOfWork
{
    public class UnitOfWork
    {
        Context _context;

        public UnitOfWork(Context context) 
        {
            _context = context;

            BeginTransaction();
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch 
            {
                _context.Database.CurrentTransaction.Rollback();
            }
        }

        public void CommitChanges()
        {
            try
            {
                _context.SaveChanges();
                _context.Database.CurrentTransaction.Commit();
            }
            catch
            {
                _context.Database.CurrentTransaction.Rollback();
            }
        }

        private void BeginTransaction()
        {
            if(_context.Database.CurrentTransaction is null) 
            {
                _context.Database.BeginTransaction();
            }
        }
    }
}
