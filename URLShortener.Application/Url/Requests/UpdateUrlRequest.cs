using MediatR;

namespace URLShortener.Application.Url.Requests
{
    public class UpdateUrlRequest : IRequest
    {
        public int Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime CreateDate { get; set; }
        public int RedirectCounter { get; set; }
    }
}
