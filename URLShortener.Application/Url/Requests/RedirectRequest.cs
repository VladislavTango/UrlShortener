using MediatR;

namespace URLShortener.Application.Url.Requests
{
    public class RedirectRequest : IRequest<string>
    {
        public string shortUrl { get; set; }
    }
}
