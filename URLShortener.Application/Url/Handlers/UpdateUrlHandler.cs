using MediatR;
using URLShortener.Application.Url.Requests;
using URLShortener.Domain.Interfaces;
using URLShortener.Domain.Models;

namespace URLShortener.Application.Url.Handlers
{
    public class UpdateUrlHandler : IRequestHandler<UpdateUrlRequest>
    {
        private readonly IUrlRepository _urlRepository;

        public UpdateUrlHandler(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task Handle(UpdateUrlRequest request, CancellationToken cancellationToken)
        {
            if (_urlRepository.GetById(request.Id) == null)
                throw new Exception("not found");
            UrlEntity entity = new()
            {
                Id = request.Id,
                LongUrl = request.LongUrl,
                ShortUrl = request.ShortUrl,
                CreateDate = request.CreateDate,
                RedirectCounter = request.RedirectCounter,
            };
            await _urlRepository.UpdateUrl(entity);
        }
    }
}
