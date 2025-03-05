using MediatR;

namespace URLShortener.Application.Url.Requests
{
    public class UpdateUrlRequest : IRequest
    {
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
    }
}
