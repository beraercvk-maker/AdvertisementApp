namespace AdvertisementApp.Entities
{

    //Şirketin sunduğu hizmetleri (örneğin "Yazılım Danışmanlığı", "Sunucu Kurulumu") listeleyecek bağımsız bir tablodur.
    public class ProvidedService : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }   
}