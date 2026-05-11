using AdvertisementApp.Dtos.Interfaces;

namespace AdvertisementApp.Dtos.AppUserDtos
{
    // Sadece IDto miras alır, çünkü bu bir oluşturma işlemidir
    public class AppUserCreateDto : IDto
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; } 
        // Not: Role (Yetki) ataması genelde Business katmanında varsayılan olarak "Member" (Üye) verilir, buraya eklenmez.
        public string Email { get; set; }
    }
}