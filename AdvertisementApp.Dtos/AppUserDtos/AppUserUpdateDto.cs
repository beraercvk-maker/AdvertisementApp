using AdvertisementApp.Dtos.Interfaces;

namespace AdvertisementApp.Dtos.AppUserDtos
{
    // IUpdateDto miras alır ki, generic servisimiz Id kolonunu tanıyabilsin
    public class AppUserUpdateDto : IUpdateDto
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public int GenderId { get; set; }
        // Not: Şifre güncelleme genelde ayrı bir form ve DTO (AppUserPasswordUpdateDto) üzerinden yapılır, buraya konmaz.
    }
}