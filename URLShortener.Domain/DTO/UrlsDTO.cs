namespace URLShortener.Domain.DTO
{
    public class UrlsDTO
    {
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public int RedirectCounter { get; set; }
    }
}
