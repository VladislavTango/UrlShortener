using MediatR;
using URLShortener.Application.Url.Requests;
using URLShortener.Domain.Interfaces;

namespace URLShortener.Application.Url.Handlers
{
    public class RedirectHandler : IRequestHandler<RedirectRequest, string>
    {
        private readonly IUrlRepository _urlRepository;

        public RedirectHandler(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<string> Handle(RedirectRequest request, CancellationToken cancellationToken)
        {
            var URL = await _urlRepository.GetByShortUrl(request.shortUrl);
            if (URL == null)
                throw new Exception("not found");

            URL.RedirectCounter += 1;
            await _urlRepository.UpdateUrl(URL);

            return URL.LongUrl;
        }
    }
}
