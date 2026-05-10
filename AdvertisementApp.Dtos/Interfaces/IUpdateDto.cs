namespace AdvertisementApp.Dtos.Interfaces
{
        //Tüm DTO'ların ortak bir arayüzü olabilir, bu da onları tanımlarken ve yönetirken tutarlılık sağlar.
        public interface IUpdateDto : IDto
        {
            int Id { get; set; }
        }
}