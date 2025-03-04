using URLShortener.Domain.Entities.Base;

namespace URLShortener.Domain.Models
{
    public class UrlEntity : BaseEntity
    {
        public virtual string LongUrl { get; set; }
        public virtual string ShortUrl { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual int RedirectCounter { get; set; }
    }
}
