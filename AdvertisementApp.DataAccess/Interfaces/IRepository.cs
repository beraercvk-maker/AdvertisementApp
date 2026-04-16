using AdvertisementApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Interfaces
{
    // <T> ifadesi bunun her tablo için (AppUser, Advertisement vb.) çalışacağını gösterir.
    // "where T : BaseEntity" kuralı ise: Sadece bizim veritabanı tablolarımız için çalışsın demektir.
    public interface IRepository<T> where T : BaseEntity
    {
        // 1. Veri Okuma Metotları (Select işlemleri)
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter); // Şartlı listeleme (Örn: Sadece aktif olanlar)
        Task<T> FindAsync(object id); // Id'ye göre tek bir kayıt bul
        Task<T> GetByFilterAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false); // Şarta göre tek kayıt bul

        // 2. Veri Ekleme Metodu (Insert)
        Task CreateAsync(T entity);

        // 3. Veri Güncelleme Metodu (Update)
        void Update(T entity);

        // 4. Veri Silme Metodu (Delete)
        void Remove(T entity);

        // 5. Özel Sorgular için Queryable (Gelişmiş listeleme işlemleri)
        IQueryable<T> GetQuery();
    }
}