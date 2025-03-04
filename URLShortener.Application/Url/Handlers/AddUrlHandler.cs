using MediatR;
using System;
using URLShortener.Application.Url.Requests;
using URLShortener.Domain.Interfaces;

namespace URLShortener.Application.Url.Handlers
{
    public class AddUrlHandler : IRequestHandler<AddUrlRequest, string>
    {
        private readonly IUrlRepository urlRepository;

        public AddUrlHandler(IUrlRepository urlRepository)
        {
            this.urlRepository = urlRepository;
        }

        public async Task<string> Handle(AddUrlRequest request, CancellationToken cancellationToken)
        {
            if (!Uri.IsWellFormedUriString(request.Url, UriKind.RelativeOrAbsolute))
                throw new Exception("Это не ссылка");

            var responce = await urlRepository.GetByLongUrl(request.Url);

            if (responce != null)
                return responce.ShortUrl;

            string shortUrl = Guid.NewGuid().ToString().Substring(0, 6);

            return await urlRepository.AddUrl(request.Url, shortUrl);

        }
    }
}
