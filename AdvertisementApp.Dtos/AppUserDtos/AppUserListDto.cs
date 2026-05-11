using AdvertisementApp.Dtos.Interfaces;

namespace AdvertisementApp.Dtos.AppUserDtos
{
    public class AppUserListDto : IDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; } // Cinsiyet bilgisini listelerken görebiliriz
        public int AppRoleId { get; set; } // Hangi yetkide olduğunu listelerken görebiliriz
    }
}