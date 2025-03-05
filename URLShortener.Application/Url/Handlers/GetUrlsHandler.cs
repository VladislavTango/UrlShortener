using MediatR;
using URLShortener.Application.Url.Reponse;
using URLShortener.Application.Url.Requests;
using URLShortener.Domain.Interfaces;

namespace URLShortener.Application.Url.Handlers
{
    public class GetUrlsHandler : IRequestHandler<GetUrlsRequest, GetUrlsResponse>
    {
        private readonly IUrlRepository _urlRepository;

        public GetUrlsHandler(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public async Task<GetUrlsResponse> Handle(GetUrlsRequest request, CancellationToken cancellationToken)
        {
            var list = await _urlRepository.GetList(request.PageNumber, request.PageSize);
            GetUrlsResponse response = new()
            {
                Urls = list,
                PageCount = await _urlRepository.GetLen(),
            };

            return response;
        }
    }
}
