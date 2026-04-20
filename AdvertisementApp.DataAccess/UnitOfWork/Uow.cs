

using AdvertisementApp.DataAccess.Context;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.DataAccess.Repositories;
using AdvertisementApp.Entities;

namespace AdvertisementApp.DataAccess.UnitOfWork
{
    public class Uow : IUow
    {
        private readonly AdvertisementContext _context;

        public Uow(AdvertisementContext context)
        {
            _context = context;
        }

        public IRepository  <T> GetRepository<T>() where T : BaseEntity
        {
            return new Repository<T>(_context);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
} 