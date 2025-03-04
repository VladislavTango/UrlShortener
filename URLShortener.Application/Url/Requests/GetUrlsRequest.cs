using MediatR;
using URLShortener.Application.Url.Reponse;

namespace URLShortener.Application.Url.Requests
{
    public class GetUrlsRequest : IRequest<GetUrlsResponse>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
