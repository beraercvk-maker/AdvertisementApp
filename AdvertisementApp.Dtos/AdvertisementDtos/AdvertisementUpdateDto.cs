using AdvertisementApp.Dtos.Interfaces; // Yine o meşhur marker interface'imizi ekliyoruz

namespace AdvertisementApp.Dtos.AdvertisementDtos
{
    public class AdvertisementUpdateDto : IUpdateDto
    {
        // Hangi kaydı güncelleyeceğimizi bilmek için Id ŞART!
        public int Id { get; set; }
        
        // Kullanıcının değiştirebileceği alanlar
        public string Title { get; set; }
        public bool Status { get; set; } // İlanın aktif/pasif durumunu güncelleyebilmesi için ekledik
        public string Description { get; set; }

        // Eğer güncelleme ekranında kullanıcının ilanı "Aktif/Pasif" yapmasına izin vereceksen:
        // public bool Status { get; set; }
    }
}