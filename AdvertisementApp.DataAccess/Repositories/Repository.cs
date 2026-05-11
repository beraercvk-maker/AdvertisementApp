using AdvertisementApp.Common.Enums;
using AdvertisementApp.DataAccess.Context;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Repositories
{
    // Sınıfın IRepository<T>'den miras aldığına ve T'nin BaseEntity olduğuna dikkat et
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly AdvertisementContext _context;

        // Dependency Injection ile DbContext'imizi içeri alıyoruz
        public Repository(AdvertisementContext context)
        {
            _context = context;
        }

        // Bütün verileri getirir. 
        // AsNoTracking() kullanıyoruz çünkü sadece listeleme yaparken EF Core'un verileri bellekte takip etmesine gerek yok (Performans artışı sağlar).
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();

            //asnotracking, sadece okuma işlemi yaparken kullanılır. Verilerin bellekte takip edilmesini engeller ve performansı artırır. Ancak, veriler üzerinde güncelleme veya silme işlemi yapacaksanız, asnoTracking kullanmamalısınız çünkü bu durumda EF Core verileri takip etmez ve değişiklikleri algılayamaz.
        }

        // Şarta göre verileri getirir (Örn: Sadece durumu aktif olan ilanlar)
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter)
        {
            return await _context.Set<T>().Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync<TKey>(Expression<Func<T, bool>> filter, Expression<Func<T, TKey>> selector, OrderByType orderByType = OrderByType.DESC)
        {
            return orderByType == OrderByType.ASC 
                ? await _context.Set<T>().Where(filter).OrderBy(selector).AsNoTracking().ToListAsync() 
                : await _context.Set<T>().Where(filter).OrderByDescending(selector).AsNoTracking().ToListAsync();
        }

        // Primary Key (Id) değerine göre tek bir veri bulur
        public async Task<T> FindAsync(object id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        // Şarta göre tek bir veri bulur. İstenirse takip (tracking) kapatılabilir.
        public async Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false)
        {
            var query = _context.Set<T>().AsQueryable();
            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.SingleOrDefaultAsync(filter);
        }

        // Yeni veri ekleme işlemi (Id atanacağı için asenkrondur)
        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        // Güncelleme işlemi. 
        // EF Core'da güncelleme işlemi aslında bellekteki verinin durumunu (State) "Modified" olarak değiştirmektir. Bu yüzden asenkron (async) değildir.
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        // Silme işlemi. Durumu "Deleted" olarak değiştirir.
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        // Daha kompleks (Include, OrderBy vb.) LINQ sorguları yazabilmek için Queryable döner
        public IQueryable<T> GetQuery()
        {
            return _context.Set<T>().AsQueryable();
        }
    }
}