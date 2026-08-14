namespace AdvertisementApp.Dtos.AdvertisementUserDtos
{
    public class AdvertisementUserCreateDto
    {
        public int AdvertisementId { get; set; }
        public int AppUserId { get; set; }
        public int AdvertisementUserStatusId { get; set; }
        public int MilitaryStatusId { get; set; }
        public int WorkExperience { get; set; }
        public string CvPath { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
