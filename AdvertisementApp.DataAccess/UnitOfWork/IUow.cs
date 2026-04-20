using AdvertisementApp.Entities;
using System.Threading.Tasks;

namespace AdvertisementApp.DataAccess.Interfaces
{
    public interface IUow
    {
        IRepository<T> GetRepository<T>() where T : BaseEntity;
        
        Task SaveChangesAsync();
        
        // Senkron işlemler için (Task olmayan) standart kaydetmeyi de ekliyoruz
        void SaveChanges(); 
    }
}